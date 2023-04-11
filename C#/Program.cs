using System;
using System.Collections.Generic;
using System.Text;

namespace calc
{
	public class storedResult
	{
		public int Count;
		public int[] Confirmed;
	}

	public class Program
	{
		static Dictionary<string, int> searchPile = new Dictionary<string, int>();
		static Dictionary<string, int> discardPile = new Dictionary<string, int>();
		static Dictionary<string, int> potentialPile = new Dictionary<string, int>();

		static readonly int[] BaseX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //0-9
		/// <summary>
		/// 5 numbers [x1][x2][x3][x4][x5] (exmpl. #12345 x1=1, x2=2 ...etc).
		/// Used for rebuild:  -1 = false, 0 = neutral
		/// </summary>
		static int[] X1 = new int[10], X2 = new int[10], X3 = new int[10], X4 = new int[10], X5 = new int[10];

		static int[] confirmedDigits = {
			-1, -1, -1, -1, -1
		};

		static string expectedResult = "00000";
		static bool debug = true;
		static bool debugNumber = true;
		static bool isRemove = false;

		static void checkKnown(string newEntry, int count, bool bypassRebuild = false)
		{
			if (isRemove)
			{
				searchPile.Remove(newEntry);
				//discardPile.Remove(newEntry);
				//potentialPile.Remove(newEntry);

				discardPile.Clear();
				potentialPile.Clear();

				//retrain fully (slow, but safe + reusable for clean restart)
				isRemove = false;
				confirmedDigits = new int[]{
					-1, -1, -1, -1, -1
				};
				X1 = new int[10];
				X2 = new int[10];
				X3 = new int[10];
				X4 = new int[10];
				X5 = new int[10];
				foreach (var item in searchPile)
                {
					checkKnown(item.Key, item.Value, true);
                }

				RebuildComparitor();
				return;
			}

			if (!bypassRebuild)
			{
				if (!searchPile.ContainsKey(newEntry)) searchPile.Add(newEntry, count); //add entered word to list with the correct count.
				else return; //already processed
			}

			//remove immediately if count is equal to 0
			if (count == 0)
			{
				discardPile.Add(newEntry, count);
			}
			else
			{
				//Check if code has invalidated points
				int[] x = new int[5];
				bool[] failsX = new bool[5];
				int failCount = 0;
				bool newPotential = false;

				x[0] = Int32.Parse(new string($"{newEntry[0]}"));
				x[1] = Int32.Parse(new string($"{newEntry[1]}"));
				x[2] = Int32.Parse(new string($"{newEntry[2]}"));
				x[3] = Int32.Parse(new string($"{newEntry[3]}"));
				x[4] = Int32.Parse(new string($"{newEntry[4]}"));

				failCount += (failsX[0] = X1[x[0]] == -1) ? 1 : 0;
				failCount += (failsX[1] = X2[x[1]] == -1) ? 1 : 0;
				failCount += (failsX[2] = X3[x[2]] == -1) ? 1 : 0;
				failCount += (failsX[3] = X4[x[3]] == -1) ? 1 : 0;
				failCount += (failsX[4] = X5[x[4]] == -1) ? 1 : 0;

				if (failsX[0] || failsX[1] || failsX[2] || failsX[3] || failsX[4])
				{
					//check if a whole number is detected.
					if (failCount + count == 5)
					{
						//set correct digits if number is known
						for (int i = 0; i < 5; i++)
						{
							if (!failsX[i]) confirmedDigits[i] = x[i];
						}
						discardPile.Add(newEntry, count);
					}
					else
					{
						//variable not yet known
						potentialPile.Add(newEntry, count);
						newPotential = true;
					}
				}
				else
				{
					potentialPile.Add(newEntry, count);
					newPotential = true;
				}
			}

			if(!bypassRebuild) RebuildComparitor();
		}

		//rebuilding the array from scratch at any request to allow for load and delete
		//solution is slower than additive solutions
		static void RebuildComparitor()
		{
			//Buffer.BlockCopy(baseComparitorArr, 0, comparitorArr, 0, baseComparitorArr.Length);
			//reset numbers
			BaseX.CopyTo(X1, 0);
			BaseX.CopyTo(X2, 0);
			BaseX.CopyTo(X3, 0);
			BaseX.CopyTo(X4, 0);
			BaseX.CopyTo(X5, 0);

			//remove all numbers know to have no corrolation
			foreach (var discardedItem in discardPile)
			{
				if (discardedItem.Value == 0)
				{
					//VVVVV conversion step skipped in JS
					int x1 = Convert.ToInt32(new string(discardedItem.Key[0], 1));
					int x2 = Convert.ToInt32(new string(discardedItem.Key[1], 1));
					int x3 = Convert.ToInt32(new string(discardedItem.Key[2], 1));
					int x4 = Convert.ToInt32(new string(discardedItem.Key[3], 1));
					int x5 = Convert.ToInt32(new string(discardedItem.Key[4], 1));
					//^^^^^^^

					X1[x1] = -1;
					X2[x2] = -1;
					X3[x3] = -1;
					X4[x4] = -1;
					X5[x5] = -1;
				}
			}
			if (confirmedDigits[0] == -1) confirmedDigits[0] = CheckRemainder(X1);
			if (confirmedDigits[1] == -1) confirmedDigits[1] = CheckRemainder(X2);
			if (confirmedDigits[2] == -1) confirmedDigits[2] = CheckRemainder(X3);
			if (confirmedDigits[3] == -1) confirmedDigits[3] = CheckRemainder(X4);
			if (confirmedDigits[4] == -1) confirmedDigits[4] = CheckRemainder(X5);

			//disable X values based on confirmed digits
			for (int i = 0; i < 10; i++)
			{
				//TODO: fix rem issue (store state?)
				if (confirmedDigits[0] != -1) X1[i] = (confirmedDigits[0] == i) ? 0 : -1;
				if (confirmedDigits[1] != -1) X2[i] = (confirmedDigits[1] == i) ? 0 : -1;
				if (confirmedDigits[2] != -1) X3[i] = (confirmedDigits[2] == i) ? 0 : -1;
				if (confirmedDigits[3] != -1) X4[i] = (confirmedDigits[3] == i) ? 0 : -1;
				if (confirmedDigits[4] != -1) X5[i] = (confirmedDigits[4] == i) ? 0 : -1;
			}
		}

		static int CheckRemainder(int[] xNUM)
		{
			int remainder = 0;
			int checkVal = 0;
			for (int i = 0; i < xNUM.Length; i++)
			{
				if (xNUM[i] != -1) checkVal = i;
				remainder = remainder - xNUM[i];
			}

			return remainder == 9 ? checkVal : -1;
		}

		static void RenderNumbers()
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < 99999; i++)
			{
				bool state = (
						X1[(i % 100000 - i % 10000) / 10000] == -1 ||
						X2[(i % 10000 - i % 1000) / 1000] == -1 ||
						X3[(i % 1000 - i % 100) / 100] == -1 ||
						X4[(i % 100 - i % 10) / 10] == -1 ||
						X5[(i % 10)] == -1
					);

				if (state == false)
				{
					sb.Append(i.ToString().PadLeft(5, '0'));
					sb.Append(' ');
				}
			}

			Console.WriteLine(sb.ToString());
		}

		public static void Main()
		{
			Random rand = new Random();
			int randomNumber = rand.Next(0, 99999);

			if (debugNumber) randomNumber = 11111;

			expectedResult = randomNumber.ToString().PadLeft(5, '0');

			BaseX.CopyTo(X1, 0);
			BaseX.CopyTo(X2, 0);
			BaseX.CopyTo(X3, 0);
			BaseX.CopyTo(X4, 0);
			BaseX.CopyTo(X5, 0);

			while (true)
			{
			redo:
				Console.Clear();
				if (debug) Console.WriteLine(expectedResult);
				Console.WriteLine("enter code:");
				string data = Console.ReadLine();
				if (data.Length < 5) goto redo;
				int dataInt = 0;
				if (isRemove = data.StartsWith('r')) data = data.Remove(0, 1);
				if (!Int32.TryParse(data, out dataInt)) goto redo;

				int countCorrect = 0;
				for (int i = 0; i < 5; i++)
				{
					if (expectedResult[i].Equals(data[i])) countCorrect++;
				}
				string d = countCorrect.ToString();

				checkKnown(data, countCorrect);

				RenderNumbers();
				Console.WriteLine("No. correct in ({1}): {0}", d, data);
				Console.WriteLine("confirmed ({0}{1}{2}{3}{4})",
					confirmedDigits[0] != -1 ? confirmedDigits[0].ToString() : '?',
					confirmedDigits[1] != -1 ? confirmedDigits[1].ToString() : '?',
					confirmedDigits[2] != -1 ? confirmedDigits[2].ToString() : '?',
					confirmedDigits[3] != -1 ? confirmedDigits[3].ToString() : '?',
					confirmedDigits[4] != -1 ? confirmedDigits[4].ToString() : '?'
					);

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
