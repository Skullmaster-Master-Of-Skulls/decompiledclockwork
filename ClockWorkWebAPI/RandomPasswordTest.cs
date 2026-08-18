using System;

namespace ClockWorkWebAPI
{
	// Token: 0x02000027 RID: 39
	public class RandomPasswordTest
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000F24C File Offset: 0x0000D44C
		[STAThread]
		private static void Main(string[] args)
		{
			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine(RandomPassword.Generate(8, 10));
			}
		}
	}
}
