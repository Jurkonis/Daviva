using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace DavivaBack
{
	class DBOldSchool
	{
		private MySqlConnection connection;
		private string server;
		private string database;
		private string uid;
		private string password;
		private string connectionString;


		//Constructor
		public DBOldSchool()
		{
			Initialize();
		}
		public DBOldSchool(string server, string database, string uid, string password)
		{
			Initialize(server, database, uid, password);
		}

		//Initialize values
		private void Initialize()
		{

			server = "178.128.202.96";
			database = "Testas";
			uid = "TestUser";
			password = "TestasTesta5";
			connectionString = "SERVER=" + server + ";port=3306;" + "DATABASE=" +
			database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Convert Zero Datetime=true;default command timeout=999;";

			connection = new MySqlConnection(connectionString);

		}
		private void Initialize(string server, string database, string uid, string password)
		{
			connectionString = "SERVER=" + server + ";" + "DATABASE=" +
			database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Convert Zero Datetime=true;default command timeout=999;";

			connection = new MySqlConnection(connectionString);

		}

		//open connection to database
		private bool OpenConnection()
		{
			try
			{
				connection.Open();
				return true;
			}
			catch
			{
				return false;
			}
		}
		public void OpenConnectionlong()
		{
			try
			{
				connection.Open();
			}
			catch
			{

			}
		}

		public string[] Getmarkes()
		{
			string query = "SELECT `Marke` FROM `Automobilis` GROUP BY `Marke`";

			MySqlDataAdapter adpt = new MySqlDataAdapter(query, connection);
			DataSet dset = new DataSet();
			adpt.Fill(dset);

			string[] markes = new string[dset.Tables[0].Rows.Count];
			for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
			{
				markes[i] = dset.Tables[0].Rows[i][0].ToString();
			}
			return markes;
		}

		public DataSet SelectAll(string table)
		{
			string query = "SELECT * FROM " + table;

			MySqlDataAdapter adpt = new MySqlDataAdapter(query, connection);
			DataSet dset = new DataSet();
			adpt.Fill(dset);
			return dset;
		}

		public string[] SelectAll()
		{
			string query = "SELECT * FROM `Automobilis`";

			MySqlDataAdapter adpt = new MySqlDataAdapter(query, connection);
			DataSet dset = new DataSet();
			adpt.Fill(dset);
			string[] auto = new string[dset.Tables[0].Rows.Count];
			for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
			{
				for (int j = 1; j < dset.Tables[0].Columns.Count; j++)
					auto[i] += " "+dset.Tables[0].Rows[i][j].ToString();
			}
			return auto;
		}
	}
}
