using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

namespace DavivaBack
{
	class Program
	{
		static string answer = "";
		static void Main(string[] args)
		{

			Console.WriteLine("3 ar 5 užduotis?");
			answer = Console.ReadLine();
			Console.Clear();

			if (answer == "5")
			{
				Task task = Task5();
				task.Wait();
			}
			else
			{
				Task3();
			}

			Console.ReadLine();
		}

		public static void Task3()
		{
			DBOldSchool db = new DBOldSchool();
			Console.WriteLine("3 užduotis");
			List<string> result = new List<string>();
			var data = db.SelectAll();
			Console.WriteLine("Įveskite žodį");
			string search = Console.ReadLine();
			string[] words = search.Split(' ');
			switch (words.Length)
			{
				case 1:
					for (int i = 0; i < data.Length; i++)
					{
						if (data[i].Contains(words[0]) && words[0]!=string.Empty)
						{
							result.Add(data[i]);
						}
					}
					break;
				case 2:
					for (int i = 0; i < data.Length; i++)
					{
						if (data[i].Contains(words[0]) && data[i].Contains(words[1]))
						{
							result.Add(data[i]);
						}
					}
					break;
				case 3:
					for (int i = 0; i < data.Length; i++)
					{
						if (data[i].Contains(words[0]) && data[i].Contains(words[1]) && data[i].Contains(words[2]))
						{
							result.Add(data[i]);
						}
					}
					break;
				default:
					throw new NotImplementedException();
			}
			if (result.Count > 0)
			{
				foreach (string s in result)
				{
					Console.WriteLine(s);
				}
			}
			else
			{
				Console.WriteLine("Klaidinga paieška");
			}
		}

		public static async Task Task5()
		{
			string path1 = "https://backend.daviva.lt/public/Markes";
			string path2 = "https://backend.daviva.lt/API/GetBrandasFromRRR"; ;
			string path3 = "https://backend.daviva.lt/API/GetCarModelsFromRRR?BrandID=";
			string path4 = "https://backend.daviva.lt/public/Modeliai?Name=";
			HttpClient client = new HttpClient();
			while (true)
			{
				// Atitinkančių markių sąrašas
				List<string> first = new List<string>();
				// Markių sąrašas, kurių nėra "...Public..." , bet yra "...API..."
				CarList second = new CarList();
				// Markių sąrašas, kurių nėra "...API.." , bet yra "...Public..."
				List<string> third = new List<string>();
				Console.WriteLine("5 užduotis");
				Console.WriteLine("Įveskite markę");
				string search = Console.ReadLine();
				// Takes data from path1
				var res = await client.GetAsync(path1);
				var data = await res.Content.ReadAsStringAsync();
				if (data.Contains(search))
				{
					// Takes data from path2
					res = await client.GetAsync(path2);
					data = await res.Content.ReadAsStringAsync();
					var carList = JsonConvert.DeserializeObject<CarList>(data);
					var a = carList.list.Find(x => x.name == search);
					if (a != null)
					{
						// Takes data from path3
						res = await client.GetAsync(path3 + a.id.ToString());
						data = await res.Content.ReadAsStringAsync();
						second = JsonConvert.DeserializeObject<CarList>(data);
						// Takes data from path4
						res = await client.GetAsync(path4 + a.name);
						data = await res.Content.ReadAsStringAsync();
						third = JsonConvert.DeserializeObject<List<string>>(data);
						// Data is sorted into lists
						for (int i = third.Count - 1; i >= 0; i--)
						{
							Car car = second.list.Find(x => x.name.Contains(third[i]));
							if (car != null)
							{
								first.Add(third[i]);
								third.RemoveAt(i);
								second.list.Remove(car);
							}
						}
						Console.WriteLine("Duomenys surušiuoti į sąrašus!");
						Console.WriteLine("Pirmo sąrašo kiekis: {0}", first.Count);
						Console.WriteLine("Antro sąrašo kiekis: {0}", second.list.Count);
						Console.WriteLine("Trečio sąrašo kiekis: {0}", third.Count);
						Console.Read();
						break;
					}
					else
					{
						Console.WriteLine("Įvyko klaida");
					}
				}
				else
				{
					Console.WriteLine("Įvyko klaida");
				}
				Console.ReadKey();
				Console.Clear();
			}
		}
	}
}
