using System;
using System.Collections.Generic;
using System.Text;

namespace calc
{
	public class Program
	{
		static List<string> basePossibilityList = new List<string>();
		static List<string> possibilitiesList = new List<string>();

		static Dictionary<string, int> searchPile = new Dictionary<string, int>();

		static Dictionary<string, int> discardPile = new Dictionary<string, int>();
		static Dictionary<string, int> potentialPile = new Dictionary<string, int>();

		//static Dictionary<string, int> modifierArr = new Dictionary<string, int>();
		static string result = "00000";
		static bool debug = true;

		static int[,,,,] baseComparitorArr = new int[10, 10, 10, 10, 10]; //used for rebuild:  -1 = false, 0 = neutral, 1 = true
		static int[,,,,] comparitorArr = new int[10, 10, 10, 10, 10]; //used for rebuild:  -1 = false, 0 = neutral, 1 = true

		static void checkKnown(string newEntry, int count)
        {
			if (!searchPile.ContainsKey(newEntry)) searchPile.Add(newEntry, count); //add entered word to list with the correct count.
			else return; //already processed

			//remove immediately if count is equal to 0
			if(count == 0)
            {
				discardPile.Add(newEntry, count);
			}

			RebuildComparitor();
		}

		//rebuilding the array from scratch at any request to allow for load and delete
		//solution is slower than additive solutions
		static void RebuildComparitor()
        {

			Buffer.BlockCopy(baseComparitorArr, 0, comparitorArr, 0, baseComparitorArr.Length);

			//remove all numbers know to have no corrolation
			foreach (var discardedItem in discardPile)
			{
				if (discardedItem.Value == 0)
				{
					//conversion step skipped in JS
					int x1 = Convert.ToInt32(new string(discardedItem.Key[0], 1));
					int x2 = Convert.ToInt32(new string(discardedItem.Key[1], 1));
					int x3 = Convert.ToInt32(new string(discardedItem.Key[2], 1));
					int x4 = Convert.ToInt32(new string(discardedItem.Key[3], 1));
					int x5 = Convert.ToInt32(new string(discardedItem.Key[4], 1));

					for (int i = 0; i < 99999; i++)
					{
						baseComparitorArr[
							x1,
							(i % 10000 - i % 1000) / 1000,
							(i % 1000 - i % 100) / 100,
							(i % 100 - i % 10) / 10,
							(i % 10)] = -1;
						baseComparitorArr[
							(i % 100000 - i % 10000) / 10000,
							x2,
							(i % 1000 - i % 100) / 100,
							(i % 100 - i % 10) / 10,
							(i % 10)] = -1;
						baseComparitorArr[
							(i % 100000 - i % 10000) / 10000,
							(i % 10000 - i % 1000) / 1000,
							x3,
							(i % 100 - i % 10) / 10,
							(i % 10)] = -1;
						baseComparitorArr[
							(i % 100000 - i % 10000) / 10000,
							(i % 10000 - i % 1000) / 1000,
							(i % 1000 - i % 100) / 100,
							x4,
							(i % 10)] = -1;
						baseComparitorArr[
							(i % 100000 - i % 10000) / 10000,
							(i % 10000 - i % 1000) / 1000,
							(i % 1000 - i % 100) / 100,
							(i % 100 - i % 10) / 10,
							x5] = -1;
					}
				}
			}
		}

		static void RenderNumbers()
        {
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < 99999; i++)
			{
				if(baseComparitorArr[
						(i % 100000 - i % 10000) / 10000,
						(i % 10000 - i % 1000) / 1000,
						(i % 1000 - i % 100) / 100,
						(i % 100 - i % 10) / 10,
						(i % 10)] != -1 )
                {
					sb.Append(i.ToString().PadLeft(5, '0'));
					sb.Append(' ');
                }
			}

			Console.WriteLine(sb.ToString());
        }

		static void RemoveDuds()
        {
			//because of future remove feature, recalc all
			possibilitiesList = new List<string>(basePossibilityList);
			

			for (int i = 0; i < 99999; i++)
			{

				basePossibilityList.Add(i.ToString().PadLeft(5, '0'));
			}
		}

		public static void Main()
		{
			Random rand = new Random();
			int randomNumber = rand.Next(0, 99999);
			result = randomNumber.ToString().PadLeft(5, '0');

			
			for (int i = 0; i < 99999; i++)
			{
				basePossibilityList.Add(i.ToString().PadLeft(5, '0'));

				baseComparitorArr[
					(i % 100000 - i % 10000) / 10000,
					(i % 10000 - i % 1000) / 1000,
					(i % 1000 - i % 100) / 100,
					(i % 100 - i % 10) / 10,
					(i % 10)] = 0;
			}

			Buffer.BlockCopy(baseComparitorArr, 0, comparitorArr, 0, baseComparitorArr.Length);

			while (true)
			{
				redo:
				Console.Clear();
				if (debug) Console.WriteLine(result);
				Console.WriteLine("enter code:");
				string data = Console.ReadLine();
				if (data.Length < 5) goto redo;
				int dataInt = 0;
				if (!Int32.TryParse(data, out dataInt)) goto redo;

				int countCorrect = 0;
				for (int i = 0; i < 5; i++)
				{
					if (result[i].Equals(data[i])) countCorrect++;
				}
				string d = countCorrect.ToString();

				checkKnown(data, countCorrect);

				RenderNumbers();
				Console.WriteLine("No. correct in ({1}): {0}", d, data);

				if (countCorrect < 5)
				{
					Console.WriteLine("retry...");
					Console.ReadLine();
				}
				else
                {
					break;
                }
			}

			Console.WriteLine("congrats");
		}
	}
}
