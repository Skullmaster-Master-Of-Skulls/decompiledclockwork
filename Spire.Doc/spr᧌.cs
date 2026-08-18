using System;
using System.Collections;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000190 RID: 400
internal class spr\u19CC
{
	// Token: 0x06000F08 RID: 3848 RVA: 0x000EE188 File Offset: 0x000ED188
	private spr\u19CC()
	{
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x000EE19C File Offset: 0x000ED19C
	internal static void ᜂ(spr\u1C2D A_0, DigitalSignatures A_1, sprά A_2)
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
		spr\u19CC.ᜁ(A_0, A_1, A_2);
		spr\u19CC.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x000EE1E8 File Offset: 0x000ED1E8
	private static void ᜁ(spr\u1C2D A_0, DigitalSignatures A_1, sprά A_2)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				int num;
				MemoryStream memoryStream;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_7C:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					memoryStream = A_0.ᜅ(ClipboardData.b("㥥ၧݩkᵭ᥯ᕱᩳ᝵౷ཹ๻᭽", a_));
					if (true)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1A2;
					case 1:
						return;
					case 2:
					{
						if (memoryStream == null)
						{
							goto IL_7C;
						}
						sprℛ sprℛ = new sprℛ(memoryStream);
						spr\u1B02 spr_u1B = sprℛ.ᜃ();
						num = 3;
						continue;
					}
					case 3:
					{
						spr\u1B02 spr_u1B;
						if (spr_u1B == null)
						{
							num = 4;
							continue;
						}
						IEnumerator enumerator = spr_u1B.GetKeyList().GetEnumerator();
						num = 0;
						continue;
					}
					case 4:
						goto IL_187;
					}
					break;
				}
			}
			return;
			IL_187:
			return;
			IL_1A2:
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 3;
						continue;
					case 3:
						goto IL_10E;
					case 4:
					{
						IEnumerator enumerator;
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						string a_2 = (string)enumerator.Current;
						sprℛ sprℛ;
						spr\u1B02 spr_u1B;
						sprᶔ.ᜀ(spr_u1B.ᜃ(a_2), new spr\u1B63(sprℛ), A_1, A_2);
						num = 2;
						continue;
					}
					}
					IL_BC:
					num = 4;
					continue;
					goto IL_BC;
				}
				IL_10E:
				return;
			}
			finally
			{
				for (;;)
				{
					IEnumerator enumerator;
					IDisposable disposable = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							disposable.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_159;
						case 2:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_15B;
						}
						break;
					}
				}
				IL_159:
				IL_15B:;
			}
			return;
		}
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x000EE3AC File Offset: 0x000ED3AC
	private static void ᜀ(spr\u1C2D A_0, DigitalSignatures A_1, sprά A_2)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			Stream stream;
			for (;;)
			{
				IL_30:
				MemoryStream memoryStream = A_0.ᜄ(ClipboardData.b("のɰᩲቴ᥶ᡸེࡼൾ", a_));
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_64;
					case 1:
						return;
					case 2:
						if (memoryStream == null)
						{
							num = 0;
							continue;
						}
						stream = memoryStream;
						num = 3;
						continue;
					case 3:
						if (stream == null)
						{
							num = 1;
							continue;
						}
						goto IL_A5;
					}
					goto IL_30;
				}
				IL_64:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_96;
				}
			}
			return;
			IL_96:
			if (true)
			{
			}
			if (false)
			{
			}
			return;
			IL_A5:
			BinaryReader binaryReader = new BinaryReader(stream, Encoding.Unicode);
			int a_2 = binaryReader.ReadInt32();
			spr\u19CC.ᜀ(binaryReader, a_2);
			binaryReader.ReadInt32();
			spr\u21AF spr_u21AF = new spr\u21AF(A_0.\u1718().ᜃ());
			spr\u19CC.ᜀ(binaryReader, A_1, spr_u21AF.ᜀ(), A_2);
			return;
		}
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x000EE4A0 File Offset: 0x000ED4A0
	private static void ᜀ(BinaryReader A_0, int A_1)
	{
		int num = 5;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
			{
				if (num2 == 0)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				A_0.ReadInt32();
				int count = A_0.ReadInt32();
				A_0.ReadBytes(count);
				num = 3;
				continue;
			}
			case 2:
				A_0.ReadInt32();
				A_0.ReadInt32();
				num = 0;
				continue;
			case 3:
				goto IL_5E;
			case 4:
				goto IL_82;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_55;
			default:
				if (false)
				{
				}
				if (A_1 > 0)
				{
					num = 2;
					continue;
				}
				return;
			}
			IL_5E:
			num2 = A_0.ReadInt32();
			num = 1;
		}
		IL_55:
		A_0.ReadBytes(8);
		return;
		IL_82:
		goto IL_55;
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x000EE578 File Offset: 0x000ED578
	private static void ᜀ(BinaryReader A_0, DigitalSignatures A_1, byte[] A_2, sprά A_3)
	{
		for (;;)
		{
			int num = A_0.ReadInt32();
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_29;
				case 1:
				{
					if (num == 0)
					{
						num2 = 2;
						continue;
					}
					DigitalSignature a_ = spr\u2295.ᜀ(A_0, A_2);
					A_1.ᜀ(a_);
					num = A_0.ReadInt32();
					if (true)
					{
					}
					num2 = 3;
					continue;
				}
				case 2:
					return;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					goto IL_29;
				}
				break;
				IL_29:
				num2 = 1;
			}
		}
	}
}
