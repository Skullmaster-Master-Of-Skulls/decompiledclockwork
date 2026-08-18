using System;
using System.Collections;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x0200007F RID: 127
	public class DigitalSignatures : IEnumerable
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00008ED8 File Offset: 0x00007ED8
		public bool IsValid
		{
			get
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					IEnumerator enumerator = this.ᜀ.GetEnumerator();
					bool result;
					try
					{
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_A5;
							case 1:
								result = false;
								num = 0;
								continue;
							case 2:
								goto IL_B0;
							case 4:
								num = 2;
								continue;
							case 5:
							{
								DigitalSignature digitalSignature;
								if (!digitalSignature.IsValid)
								{
									num = 1;
									continue;
								}
								break;
							}
							case 6:
							{
								if (!enumerator.MoveNext())
								{
									num = 4;
									continue;
								}
								DigitalSignature digitalSignature = (DigitalSignature)enumerator.Current;
								num = 5;
								continue;
							}
							}
							IL_56:
							num = 6;
							continue;
							goto IL_56;
						}
						IL_A5:
						return result;
						IL_B0:
						return true;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							int num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_113;
								case 1:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								case 2:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_115;
								}
								break;
							}
						}
						IL_113:
						IL_115:;
					}
					return result;
				}
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00009018 File Offset: 0x00008018
		public int Count
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Count;
			}
		}

		// Token: 0x17000029 RID: 41
		public DigitalSignature this[int index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return (DigitalSignature)this.ᜀ[index];
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000090AC File Offset: 0x000080AC
		internal void ᜀ(DigitalSignature A_0)
		{
			for (;;)
			{
				this.ᜀ.Add(A_0);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0.Visible)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜁ.Add(A_0.SetupId, A_0);
							if (true)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00009148 File Offset: 0x00008148
		internal DigitalSignature ᜀ(string A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return (DigitalSignature)this.ᜁ[new Guid(A_0)];
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000091A0 File Offset: 0x000081A0
		public IEnumerator GetEnumerator()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ.GetEnumerator();
		}

		// Token: 0x04000843 RID: 2115
		private bool \u25D8\u00AF\u009B\u00A8;

		// Token: 0x04000844 RID: 2116
		private int[] \u25D8\u00A3\u0091\u00A6;

		// Token: 0x04000845 RID: 2117
		private byte[] \u25D9\u0094\u008B\u0098;

		// Token: 0x04000846 RID: 2118
		private string \u25D8\u0082\u009A\u0091;

		// Token: 0x04000847 RID: 2119
		private readonly ArrayList ᜀ = new ArrayList();

		// Token: 0x04000848 RID: 2120
		private readonly Hashtable ᜁ = new Hashtable();
	}
}
