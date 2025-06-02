  let socket = io();
    let currentCode = "";
    let currentQuestionId = null;
    let joined = false;

    function joinQuiz() {
      currentCode = document.getElementById('code').value.trim().toUpperCase();
      if (!currentCode) return alert('Bitte einen Code eingeben');

      socket.emit('join-quiz', { code: currentCode });
      document.getElementById('join-block').style.display = 'none';
      document.getElementById('quiz-block').style.display = 'block';
      document.getElementById('question').textContent = 'Bitte warten Sie auf die Freigabe der ersten Frage...';
      // Fehlertext leeren
      document.getElementById('error-message').textContent = '';
    }

    socket.on('show-question', data => {
      if (!joined) {
        joined = true;
        document.getElementById('join-block').style.display = 'none';
        document.getElementById('quiz-block').style.display = 'block';

        // Titel aktualisieren mit Kategorie (falls vorhanden)
        const category = data.category || '';
        if (category) {
          document.getElementById('quiz-title').textContent = 'Quiz: ' + category;
        }
      }

      currentQuestionId = data.id;
      document.getElementById('question').textContent = data.text;
      document.getElementById('submitted-block').style.display = 'none';

      const optionTexts = document.getElementById('option-texts');
      const answerButtons = document.getElementById('answer-buttons');
      optionTexts.innerHTML = '';
      answerButtons.innerHTML = '';

      ['A', 'B', 'C', 'D'].forEach(letter => {
        const text = data[`option${letter}`];
        if (text) {
          const p = document.createElement('p');
          p.className = 'option-text';
          p.textContent = `${letter}: ${text}`;
          optionTexts.appendChild(p);

          const btn = document.createElement('button');
          btn.textContent = letter;
          btn.onclick = () => submitAnswer(letter);
          btn.id = `btn-${letter}`;
          answerButtons.appendChild(btn);
        }
      });
    });

    socket.on('error-message', msg => {
      document.getElementById('error-message').textContent = msg;
      document.getElementById('join-block').style.display = 'block';
    });

    function submitAnswer(option) {
      if (!currentCode || !currentQuestionId) return;

      socket.emit('answer', {
        code: currentCode,
        questionId: currentQuestionId,
        option
      });

      document.getElementById('submitted-answer').textContent = option;
      document.getElementById('submitted-block').style.display = 'block';

      ['A', 'B', 'C', 'D'].forEach(letter => {
        const btn = document.getElementById(`btn-${letter}`);
        if (btn) {
          btn.disabled = true;
          if (letter !== option) {
            btn.style.opacity = 0.5;
          }
        }
      });
    }