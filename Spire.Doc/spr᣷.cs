using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;
using Spire.Doc;

// Token: 0x02000447 RID: 1095
[DefaultMember("Item")]
internal class spr\u18F7
{
	// Token: 0x06003CEF RID: 15599 RVA: 0x0038C9A0 File Offset: 0x0038B9A0
	internal sprᠾ ᜀ(int A_0)
	{
		if (this.ᜀ.ContainsKey(A_0))
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return this.ᜀ[A_0];
			}
		}
		return null;
	}

	// Token: 0x06003CF0 RID: 15600 RVA: 0x0038C9FC File Offset: 0x0038B9FC
	internal Document ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x06003CF1 RID: 15601 RVA: 0x0038CA40 File Offset: 0x0038BA40
	internal spr\u18F7(Document A_0)
	{
		this.ᜃ = A_0;
	}

	// Token: 0x06003CF2 RID: 15602 RVA: 0x0038CA70 File Offset: 0x0038BA70
	internal void ᜀ(sprᠾ A_0)
	{
		int num;
		for (;;)
		{
			num = 1;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_D2;
				case 1:
					goto IL_60;
				case 2:
					goto IL_F5;
				case 3:
					if (this.ᜁ.Count > 0)
					{
						num2 = 6;
						continue;
					}
					num2 = 4;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (this.ᜀ.Count > 0)
						{
							num2 = 5;
							continue;
						}
						this.ᜂ++;
						break;
					}
					num2 = 2;
					continue;
				case 5:
					num = ++this.ᜂ;
					num2 = 1;
					continue;
				case 6:
					num = this.ᜁ[0];
					this.ᜁ.RemoveAt(0);
					num2 = 0;
					continue;
				}
				break;
			}
		}
		IL_60:
		goto IL_F7;
		IL_D2:
		if (true)
		{
		}
		IL_F5:
		IL_F7:
		A_0.ᜀ(num);
		this.ᜀ.Add(num, A_0);
	}

	// Token: 0x06003CF3 RID: 15603 RVA: 0x0038CB88 File Offset: 0x0038BB88
	internal bool ᜁ(int A_0)
	{
		if (this.ᜀ.ContainsKey(A_0))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.Remove(A_0);
				this.ᜁ.Add(A_0);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003CF4 RID: 15604 RVA: 0x0038CBF0 File Offset: 0x0038BBF0
	internal void ᜀ()
	{
		Dictionary<int, sprᠾ>.ValueCollection.Enumerator enumerator = this.ᜀ.Values.GetEnumerator();
		try
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
				{
					if (!enumerator.MoveNext())
					{
						num = 3;
						continue;
					}
					sprᠾ sprᠾ = enumerator.Current;
					sprᠾ.ᜈ();
					num = 0;
					continue;
				}
				case 3:
					goto IL_88;
				case 4:
					goto IL_90;
				}
				IL_6D:
				num = 1;
				continue;
				goto IL_6D;
				IL_88:
				num = 4;
			}
			IL_90:;
		}
		finally
		{
			if (true)
			{
			}
			((IDisposable)enumerator).Dispose();
		}
		this.ᜀ.Clear();
		this.ᜁ.Clear();
		this.ᜂ = 0;
	}

	// Token: 0x06003CF5 RID: 15605 RVA: 0x0038CCDC File Offset: 0x0038BCDC
	internal sprᠾ ᜂ(byte[] A_0)
	{
		switch (0)
		{
		default:
		{
			sprᠾ sprᠾ;
			for (;;)
			{
				HMACSHA1 hmacsha = new HMACSHA1();
				hmacsha.Key = sprᠾ.ᜀ;
				spr\u1AED spr_u1AED = new spr\u1AED();
				sprᠾ = null;
				Dictionary<int, sprᠾ>.ValueCollection.Enumerator enumerator = this.ᜀ.Values.GetEnumerator();
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (sprᠾ == null)
						{
							num = 2;
							continue;
						}
						goto IL_1D3;
					case 1:
						try
						{
							num = 1;
							for (;;)
							{
								sprᠾ sprᠾ2;
								switch (num)
								{
								case 0:
									goto IL_F9;
								case 2:
									goto IL_19E;
								case 3:
									goto IL_192;
								case 4:
									sprᠾ = sprᠾ2;
									num = 10;
									continue;
								case 5:
									num = 7;
									continue;
								case 6:
									num = 9;
									continue;
								case 7:
									if (sprᠾ2.ᜂ.Length == A_0.Length)
									{
										num = 6;
										continue;
									}
									goto IL_119;
								case 8:
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									sprᠾ2 = enumerator.Current;
									num = 0;
									continue;
								case 9:
									if (spr_u1AED.ᜀ(sprᠾ2.ᜇ(), hmacsha.ComputeHash(A_0)))
									{
										num = 4;
										continue;
									}
									goto IL_119;
								case 10:
									goto IL_192;
								}
								goto IL_CA;
								IL_F9:
								if (!sprᠾ2.ᜄ())
								{
									num = 5;
									continue;
								}
								goto IL_119;
								IL_CA:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_F9;
								default:
									if (false)
									{
									}
									break;
								}
								IL_119:
								num = 8;
								continue;
								IL_192:
								num = 2;
							}
							IL_19E:
							goto IL_6F;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_1B1;
						IL_6F:
						num = 0;
						continue;
					case 2:
						goto IL_1B1;
					case 3:
						goto IL_1D1;
					}
					break;
					IL_1B1:
					sprᠾ = new sprᠾ(this.ᜃ, A_0);
					this.ᜀ(sprᠾ);
					num = 3;
				}
			}
			IL_1D1:
			IL_1D3:
			A_0 = null;
			sprᠾ sprᠾ3 = sprᠾ;
			sprᠾ3.ᜂ(sprᠾ3.ᜅ() + 1);
			return sprᠾ;
		}
		}
	}

	// Token: 0x06003CF6 RID: 15606 RVA: 0x0038CEEC File Offset: 0x0038BEEC
	internal sprᠾ ᜀ(byte[] A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			sprᠾ sprᠾ;
			for (;;)
			{
				int a_ = A_0.Length;
				int num = 2;
				for (;;)
				{
					spr\u1AED spr_u1AED;
					HMACSHA1 hmacsha;
					Dictionary<int, sprᠾ>.ValueCollection.Enumerator enumerator;
					switch (num)
					{
					case 0:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_22F;
								case 1:
									num = 9;
									continue;
								case 3:
									goto IL_22F;
								case 4:
								{
									sprᠾ sprᠾ2;
									sprᠾ = sprᠾ2;
									num = 3;
									continue;
								}
								case 5:
									goto IL_23B;
								case 6:
								{
									sprᠾ sprᠾ2;
									if (sprᠾ2.ᜄ())
									{
										num = 7;
										continue;
									}
									goto IL_1D0;
								}
								case 7:
									num = 8;
									continue;
								case 8:
								{
									sprᠾ sprᠾ2;
									if (sprᠾ2.ᜂ.Length == A_0.Length)
									{
										num = 1;
										continue;
									}
									goto IL_1D0;
								}
								case 9:
								{
									sprᠾ sprᠾ2;
									if (spr_u1AED.ᜀ(sprᠾ2.ᜇ(), hmacsha.ComputeHash(A_0)))
									{
										goto IL_199;
									}
									goto IL_1D0;
								}
								case 10:
								{
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									sprᠾ sprᠾ2 = enumerator.Current;
									num = 6;
									continue;
								}
								}
								goto IL_15C;
								IL_199:
								num = 4;
								continue;
								IL_15C:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_199;
								default:
									if (false)
									{
									}
									break;
								}
								IL_1D0:
								num = 10;
								continue;
								IL_22F:
								num = 5;
							}
							IL_23B:
							goto IL_56;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_24E;
						IL_56:
						num = 1;
						continue;
					case 1:
						if (sprᠾ == null)
						{
							num = 7;
							continue;
						}
						goto IL_24E;
					case 2:
						if (!A_1)
						{
							num = 6;
							continue;
						}
						goto IL_7A;
					case 3:
						goto IL_C6;
					case 4:
						goto IL_7A;
					case 5:
						if (!A_1)
						{
							num = 8;
							continue;
						}
						goto IL_24E;
					case 6:
						A_0 = this.ᜀ(A_0);
						num = 4;
						continue;
					case 7:
						sprᠾ = new sprᠾ(this.ᜃ, A_0);
						this.ᜀ(sprᠾ);
						num = 5;
						continue;
					case 8:
						sprᠾ.ᜁ(a_);
						num = 3;
						continue;
					}
					break;
					IL_7A:
					hmacsha = new HMACSHA1();
					hmacsha.Key = sprᠾ.ᜀ;
					spr_u1AED = new spr\u1AED();
					sprᠾ = null;
					enumerator = this.ᜀ.Values.GetEnumerator();
					num = 0;
				}
			}
			IL_C6:
			if (true)
			{
			}
			IL_24E:
			A_0 = null;
			sprᠾ sprᠾ3 = sprᠾ;
			sprᠾ3.ᜂ(sprᠾ3.ᜅ() + 1);
			sprᠾ.ᜀ(true);
			return sprᠾ;
		}
		}
	}

	// Token: 0x06003CF7 RID: 15607 RVA: 0x0038D17C File Offset: 0x0038C17C
	internal sprᠾ ᜃ(byte[] A_0)
	{
		sprᠾ result;
		for (;;)
		{
			Image image = sprᠾ.ᜀ(A_0);
			result = null;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_53;
				case 1:
					if (image is Metafile)
					{
						num = 2;
						continue;
					}
					result = this.ᜂ(A_0);
					num = 0;
					continue;
				case 2:
					result = this.ᜀ(A_0, false);
					num = 3;
					continue;
				case 3:
					goto IL_66;
				}
				break;
			}
		}
		IL_53:
		IL_66:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_66;
		default:
			if (false)
			{
			}
			return result;
		}
	}

	// Token: 0x06003CF8 RID: 15608 RVA: 0x0038D218 File Offset: 0x0038C218
	private byte[] ᜀ(byte[] A_0)
	{
		byte[] result;
		try
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			MemoryStream memoryStream = new MemoryStream();
			spr\u234C spr_u234C = new spr\u234C(memoryStream, true);
			spr_u234C.ᜂ(A_0, 0, A_0.Length, true);
			memoryStream.Close();
			result = memoryStream.ToArray();
		}
		catch
		{
			MemoryStream memoryStream2 = new MemoryStream();
			GZipStream gzipStream = new GZipStream(memoryStream2, CompressionMode.Compress, true);
			gzipStream.Write(A_0, 0, A_0.Length);
			gzipStream.Close();
			result = memoryStream2.ToArray();
			memoryStream2.Close();
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06003CF9 RID: 15609 RVA: 0x0038D2C0 File Offset: 0x0038C2C0
	internal byte[] ᜁ(byte[] A_0)
	{
		byte[] result;
		try
		{
			switch (0)
			{
			default:
				for (;;)
				{
					MemoryStream memoryStream = new MemoryStream(A_0);
					sprᢹ sprᢹ = new sprᢹ(memoryStream);
					MemoryStream memoryStream2 = new MemoryStream();
					byte[] array = new byte[4096];
					int num = 4;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							goto IL_C2;
						case 1:
							if (num2 > 0)
							{
								num = 3;
								continue;
							}
							memoryStream.Close();
							memoryStream = null;
							result = memoryStream2.ToArray();
							memoryStream2.Close();
							memoryStream2 = null;
							num = 0;
							continue;
						case 2:
							goto IL_74;
						case 3:
							memoryStream2.Write(array, 0, num2);
							num = 2;
							continue;
						case 4:
							if (true)
							{
							}
							goto IL_74;
						}
						break;
						IL_74:
						num2 = sprᢹ.ᜀ(array, 0, array.Length);
						num = 1;
					}
				}
				IL_C2:
				break;
			}
		}
		catch
		{
			GZipStream gzipStream = new GZipStream(new MemoryStream(A_0), CompressionMode.Decompress, true);
			try
			{
				byte[] array2 = new byte[4096];
				MemoryStream memoryStream3 = new MemoryStream();
				try
				{
					for (;;)
					{
						int num3 = 0;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								memoryStream3.Write(array2, 0, num3);
								num = 4;
								continue;
							case 1:
								goto IL_151;
							case 2:
								if (num3 <= 0)
								{
									goto IL_121;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1A1;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 3:
								result = memoryStream3.ToArray();
								goto IL_1A1;
							case 4:
								goto IL_121;
							case 5:
								if (num3 <= 0)
								{
									num = 3;
									continue;
								}
								goto IL_151;
							case 6:
								goto IL_1AD;
							}
							break;
							IL_121:
							num = 5;
							continue;
							IL_151:
							num3 = gzipStream.Read(array2, 0, array2.Length);
							num = 2;
							continue;
							IL_1A1:
							num = 6;
						}
					}
					IL_1AD:;
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1EC;
						case 2:
							((IDisposable)memoryStream3).Dispose();
							num = 0;
							continue;
						}
						if (memoryStream3 == null)
						{
							break;
						}
						num = 2;
					}
					IL_1EC:;
				}
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_22E;
					case 2:
						((IDisposable)gzipStream).Dispose();
						num = 1;
						continue;
					}
					if (gzipStream == null)
					{
						break;
					}
					num = 2;
				}
				IL_22E:;
			}
		}
		return result;
	}

	// Token: 0x04002C10 RID: 11280
	private Dictionary<int, sprᠾ> ᜀ = new Dictionary<int, sprᠾ>();

	// Token: 0x04002C11 RID: 11281
	private List<int> ᜁ = new List<int>();

	// Token: 0x04002C12 RID: 11282
	private int ᜂ;

	// Token: 0x04002C13 RID: 11283
	private Document ᜃ;
}
