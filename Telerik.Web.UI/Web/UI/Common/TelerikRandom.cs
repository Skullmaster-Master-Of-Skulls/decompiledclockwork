using System;
using System.Security.Cryptography;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02000E84 RID: 3716
	[Serializable]
	public class TelerikRandom : IDisposable
	{
		// Token: 0x17002C84 RID: 11396
		// (get) Token: 0x06008CEC RID: 36076 RVA: 0x00200039 File Offset: 0x001FE239
		// (set) Token: 0x06008CED RID: 36077 RVA: 0x00200054 File Offset: 0x001FE254
		private RNGCryptoServiceProvider RandomNumberGenerator
		{
			get
			{
				if (this._rndNumGen == null)
				{
					this._rndNumGen = new RNGCryptoServiceProvider();
				}
				return this._rndNumGen;
			}
			set
			{
				this._rndNumGen = value;
			}
		}

		// Token: 0x06008CEE RID: 36078 RVA: 0x0020005D File Offset: 0x001FE25D
		public int GetInt(int maxValue)
		{
			return this.GetInt(0, maxValue);
		}

		// Token: 0x06008CEF RID: 36079 RVA: 0x00200068 File Offset: 0x001FE268
		public int GetInt(int minValue, int maxValue)
		{
			if (minValue == maxValue)
			{
				return minValue;
			}
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("invalid arguments");
			}
			byte[] array = new byte[8];
			this.RandomNumberGenerator.GetBytes(array);
			long num = Math.Abs(BitConverter.ToInt64(array, 0));
			long num2 = Math.Abs((long)maxValue - (long)minValue);
			num %= num2;
			return Convert.ToInt32((long)minValue + num);
		}

		// Token: 0x06008CF0 RID: 36080 RVA: 0x002000C4 File Offset: 0x001FE2C4
		public void GetBytes(byte[] array)
		{
			this.RandomNumberGenerator.GetBytes(array);
		}

		// Token: 0x06008CF1 RID: 36081 RVA: 0x002000D4 File Offset: 0x001FE2D4
		public double GetDouble()
		{
			TelerikRandom telerikRandom = new TelerikRandom();
			int @int = telerikRandom.GetInt(100000);
			return (double)@int / 100000.0;
		}

		// Token: 0x06008CF2 RID: 36082 RVA: 0x00200101 File Offset: 0x001FE301
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06008CF3 RID: 36083 RVA: 0x0020010A File Offset: 0x001FE30A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._rndNumGen != null)
				{
					this._rndNumGen.Dispose();
				}
				if (this.RandomNumberGenerator != null)
				{
					this.RandomNumberGenerator.Dispose();
				}
			}
		}

		// Token: 0x04002793 RID: 10131
		[NonSerialized]
		private RNGCryptoServiceProvider _rndNumGen;
	}
}
