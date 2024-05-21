
using System.Data;
using System.Windows.Forms;

namespace SQL_Lernen
{
    public partial class Form1 : Form
    {
        Database database;
        Logging logger;
        public Form1()
        {
            InitializeComponent();
            lbl_Info.Text = "";
            InitializeDatabase();
            InitializeTreeViewDatabase();
            logger = new Logging(Environment.CurrentDirectory + "\\Log.log");
            logger.Log("App gestartet",LogType.System);
            dataGridView1.AllowUserToAddRows = false;
        }

        private void InitializeDatabase()
        {
            database = new Database();

        }
        private void InitializeTreeViewDatabase()
        {
            treeViewDatabase.Nodes.Clear();
            List<string> databaseList = new List<string>();
            databaseList = database.GetAllTableNames();

            treeViewDatabase.BeginUpdate();
            treeViewDatabase.Nodes.Add("Datenbank");
            foreach (string table in databaseList)
            {
                TreeNode parentNode = new TreeNode(table);

                foreach (string column in database.GetAllColumnNames(table))
                {
                    parentNode.Nodes.Add(column);
                }
                treeViewDatabase.Nodes.Add(parentNode);
            }
            treeViewDatabase.EndUpdate();
            treeViewDatabase.ExpandAll();
        }

        private void btn_executeSQL_Click(object sender, EventArgs e)
        {
            if(InputBox.Text != "")
            {
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                lbl_Info.Text = "";
                logger.Log(InputBox.Text, LogType.Input);

                if (InputBox.Text.Substring(0, 6).ToUpper() == "SELECT")
                {
                    DataTable dataTable = database.GetDatatable(InputBox.Text);
                    if (dataTable != null)
                    {
                        BindingSource bs = new BindingSource();
                        bs.DataSource = dataTable;
                        dataGridView1.DataSource = bs;
                        logger.Log(logger.DGVtoString(dataGridView1, '|'), LogType.Output);
                    }
                    else
                    {
                        MessageBox.Show(database.LastError, "Fehler in SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        logger.Log(database.LastError, LogType.Error);
                    }

                }
                else
                {
                    int alteredColumns = database.ExecuteNonQuery(InputBox.Text);
                    if (alteredColumns > 0)
                    {
                        lbl_Info.Text = alteredColumns.ToString() + " Zeilen betroffen";
                        logger.Log(alteredColumns.ToString() + " Zeilen betroffen", LogType.Output);
                    }
                    else
                    {
                        MessageBox.Show(database.LastError, "Fehler in SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        logger.Log(database.LastError, LogType.Error);
                    }

                }
            }
            


        }



        private void sELECTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox.Text = @"SELECT column1, column2, ...
FROM table_name;";
        }

        private void uPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox.Text = @"UPDATE table_name
SET column1 = value1, column2 = value2, ...
WHERE condition;";
        }

        private void iNSERTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox.Text = @"INSERT INTO table_name (column1, column2, column3, ...)
VALUES (value1, value2, value3, ...);";
        }

        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox.Text = @"DELETE FROM 
 table_name 
WHERE condition;";
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aufWerkseinstellungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklich die komplette Datenbank zurücksetzen?", "Werksreset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                database.ResetDatabase();
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                InitializeTreeViewDatabase();
            }

        }

        private void sichernBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.BackupDatabase();
            MessageBox.Show("Backup erstellt:\r" + Environment.CurrentDirectory + "\\Backup.db");
        }

        private void wiederherstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "database files (*.db)|*.db|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                database.ImportDatabase(ofd.FileName);
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                InitializeTreeViewDatabase();
            }
        }
    }
}
