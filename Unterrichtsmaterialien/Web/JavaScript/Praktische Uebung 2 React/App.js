import React, { useState } from 'react';
import './App.css';

function App() {
  const [tasks, setTasks] = useState([]); // State für Aufgaben
  const [task, setTask] = useState(''); // State für die Eingabe

  const addTask = () => {
    if (task.trim()) {
      setTasks([...tasks, task]); // Aufgabe hinzufügen
      setTask(''); // Eingabefeld leeren
    }
  };

  const deleteTask = (index) => {
    setTasks(tasks.filter((_, i) => i !== index)); // Aufgabe entfernen
  };

  return (
    <div className="App">
      <h1>React To-Do-Liste</h1>
      <input
        type="text"
        value={task}
        onChange={(e) => setTask(e.target.value)}
        placeholder="Neue Aufgabe eingeben"
      />
      <button onClick={addTask}>Hinzufügen</button>
      <ul>
        {tasks.map((t, index) => (
          <li key={index}>
            {t}{' '}
            <button onClick={() => deleteTask(index)}>Löschen</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
