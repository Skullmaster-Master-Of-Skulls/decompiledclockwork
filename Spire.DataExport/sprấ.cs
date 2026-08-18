using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.XLS.IO;

// Token: 0x0200010A RID: 266
internal class sprấ : sprḗ
{
	// Token: 0x060005CD RID: 1485 RVA: 0x00037D84 File Offset: 0x00036D84
	public sprấ(Stream A_0)
	{
		this.ᜃ = true;
		this.ᜂ = A_0;
		Path.GetTempFileName();
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00037DAC File Offset: 0x00036DAC
	public sprấ(string A_0)
	{
		int a_ = 11;
		base..ctor();
		OLE_MODE a_2 = OLE_MODE.STGM_READWRITE | OLE_MODE.STGM_SHARE_EXCLUSIVE | OLE_MODE.STGM_CREATE;
		this.ᜃ = false;
		SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
		try
		{
			securityPermission.Assert();
			sprấ.ᜀ(A_0, (int)a_2, 0, out this.ᜀ);
			goto IL_55;
		}
		catch
		{
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("渦䜨崪䰬䌮堰圲稴䜶尸䤺尼䬾⡀ⱂ⭄ᡆ᱈╊⁌⹎㽐㉒㉔㉖㵘ᡚ㉜㭞Ѡ", a_)));
		}
		goto IL_94;
		IL_55:
		if (this.ᜀ == null)
		{
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("渦䜨崪䰬䌮堰圲稴䜶尸䤺尼䬾⡀ⱂ⭄ᡆੈ㥊⡌⹎═㙒ᩔ㕖㍘㹚㹜⭞", a_)));
		}
		a_2 = (OLE_MODE.STGM_READWRITE | OLE_MODE.STGM_SHARE_EXCLUSIVE);
		this.ᜀ.ᜀ(HyperlinksCollectionEditor.b("瀦䘨太䘬䴮帰尲帴", a_), (uint)a_2, 0U, 0U, out this.ᜁ);
		if (this.ᜁ != null)
		{
			return;
		}
		IL_94:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("渦䜨崪䰬䌮堰圲稴䜶尸䤺尼䬾⡀ⱂ⭄ᡆੈ㥊⡌⹎═㙒ٔ⍖⭘㹚㱜㉞", a_)));
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00037E9C File Offset: 0x00036E9C
	public virtual void ᜁ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5A;
			case 2:
				goto IL_98;
			}
			if (true)
			{
			}
			if (this.ᜃ)
			{
				break;
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
				num = 0;
				continue;
			}
			IL_5A:
			this.ᜁ.Commit(0);
			this.ᜀ.ᜀ(0U);
			Marshal.ReleaseComObject(this.ᜁ);
			Marshal.ReleaseComObject(this.ᜀ);
			num = 2;
		}
		IL_98:
		base.Close();
	}

	// Token: 0x060005D0 RID: 1488
	[DllImport("ole32", CharSet = CharSet.Unicode)]
	private static extern void StgCreateDocfile(string A_0, int A_1, int A_2, out sprᢗ A_3);

	// Token: 0x060005D1 RID: 1489 RVA: 0x00037F4C File Offset: 0x00036F4C
	private static void ᜀ(string A_0, int A_1, int A_2, out sprᢗ A_3)
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
		sprấ.StgCreateDocfile(A_0, A_1, A_2, out A_3);
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00037F90 File Offset: 0x00036F90
	public UCOMIStream ᜂ()
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
		return this.ᜁ;
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00037FD4 File Offset: 0x00036FD4
	public unsafe virtual int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int* ptr;
		for (;;)
		{
			IL_00:
			for (;;)
			{
				int num = 0;
				ptr = &num;
				int num2 = 0;
				for (;;)
				{
					GCHandle gchandle;
					switch (num2)
					{
					case 0:
						if (this.ᜃ)
						{
							num2 = 1;
							continue;
						}
						goto IL_51;
					case 1:
						goto IL_32;
					case 2:
						if (true)
						{
						}
						try
						{
							this.ᜁ.Read(A_0, A_2, (IntPtr)((void*)ptr));
							goto IL_A3;
						}
						finally
						{
							gchandle.Free();
						}
						goto IL_51;
					}
					break;
					IL_51:
					gchandle = GCHandle.Alloc(A_0, GCHandleType.Pinned);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num2 = 2;
						break;
					}
				}
			}
		}
		IL_32:
		return this.ᜂ.Read(A_0, A_1, A_2);
		IL_A3:
		return *ptr;
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x00038098 File Offset: 0x00037098
	public override int ᜀ(byte[] A_0, int A_1)
	{
		if (!this.ᜃ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return this.Read(A_0, 0, A_1);
			}
		}
		return this.ᜂ.Read(A_0, 0, A_1);
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x000380F8 File Offset: 0x000370F8
	public virtual void ᜁ(byte[] A_0, int A_1, int A_2)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				GCHandle gchandle;
				switch (num)
				{
				case 1:
					try
					{
						this.ᜁ.Write(A_0, A_2, (IntPtr)0);
						return;
					}
					finally
					{
						gchandle.Free();
					}
					goto IL_79;
				case 2:
					goto IL_50;
				}
				if (this.ᜃ)
				{
					num = 2;
					continue;
				}
				IL_79:
				gchandle = GCHandle.Alloc(A_0, GCHandleType.Pinned);
				num = 1;
			}
			IL_50:
			break;
		}
		}
		this.ᜂ.Write(A_0, 0, A_2);
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x000381B0 File Offset: 0x000371B0
	public unsafe override int ᜁ(byte[] A_0, int A_1)
	{
		int* ptr;
		for (;;)
		{
			IL_00:
			for (;;)
			{
				int num = 0;
				ptr = &num;
				int num2 = 2;
				for (;;)
				{
					GCHandle gchandle;
					switch (num2)
					{
					case 0:
						goto IL_3A;
					case 1:
						try
						{
							this.ᜁ.Write(A_0, A_1, (IntPtr)((void*)ptr));
							goto IL_A4;
						}
						finally
						{
							gchandle.Free();
						}
						goto IL_59;
					case 2:
						if (true)
						{
						}
						if (this.ᜃ)
						{
							num2 = 0;
							continue;
						}
						goto IL_59;
					}
					break;
					IL_59:
					gchandle = GCHandle.Alloc(A_0, GCHandleType.Pinned);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					}
					if (false)
					{
					}
					num2 = 1;
				}
			}
		}
		IL_3A:
		this.ᜂ.Write(A_0, 0, A_1);
		return A_1;
		IL_A4:
		return *ptr;
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00038274 File Offset: 0x00037274
	public unsafe override long ᜀ(long A_0, SeekOrigin A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			int dwOrigin;
			long* ptr;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AF;
					}
					goto Block_2;
				case 1:
					switch (A_1)
					{
					case SeekOrigin.Begin:
						dwOrigin = 0;
						num = 3;
						continue;
					case SeekOrigin.Current:
						dwOrigin = 1;
						num = 5;
						continue;
					case SeekOrigin.End:
						if (true)
						{
						}
						dwOrigin = 2;
						goto IL_AF;
					default:
						num = 7;
						continue;
					}
					break;
				case 2:
					goto IL_6C;
				case 3:
					goto IL_105;
				case 4:
					goto IL_BB;
				case 5:
					goto IL_5E;
				case 7:
					num = 2;
					continue;
				}
				if (this.ᜃ)
				{
					num = 0;
					continue;
				}
				long num2 = 0L;
				ptr = &num2;
				dwOrigin = 0;
				num = 1;
				continue;
				IL_AF:
				num = 4;
			}
			IL_5E:
			IL_6C:
			goto IL_107;
			Block_2:
			if (false)
			{
			}
			return this.ᜂ.Seek(A_0, A_1);
			IL_BB:
			IL_105:
			IL_107:
			this.ᜁ.Seek(A_0, dwOrigin, (IntPtr)((void*)ptr));
			return *ptr;
		}
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x000383A0 File Offset: 0x000373A0
	public virtual void ᜆ()
	{
		if (!this.ᜃ)
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
				this.ᜁ.Commit(0);
				return;
			}
		}
		if (true)
		{
		}
		this.ᜂ.Flush();
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00038400 File Offset: 0x00037400
	public virtual void ᜁ(long A_0)
	{
		if (!this.ᜃ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜁ.SetSize(A_0);
				return;
			}
		}
		this.ᜂ.SetLength(A_0);
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00038460 File Offset: 0x00037460
	public virtual bool ᜀ()
	{
		if (true)
		{
		}
		if (!this.ᜃ)
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
				return true;
			}
		}
		return this.ᜂ.CanRead;
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x000384B4 File Offset: 0x000374B4
	public virtual bool ᜄ()
	{
		if (!this.ᜃ)
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
				return true;
			}
		}
		if (true)
		{
		}
		return this.ᜂ.CanWrite;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00038508 File Offset: 0x00037508
	public virtual bool ᜃ()
	{
		if (!this.ᜃ)
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
				return true;
			}
		}
		if (true)
		{
		}
		return this.ᜂ.CanSeek;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0003855C File Offset: 0x0003755C
	public virtual long ᜇ()
	{
		if (!this.ᜃ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				STATSTG statstg;
				this.ᜁ.Stat(out statstg, 0);
				return statstg.cbSize;
			}
			}
		}
		if (true)
		{
		}
		return this.ᜂ.Length;
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x000385C4 File Offset: 0x000375C4
	public virtual long ᜅ()
	{
		if (!this.ᜃ)
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
				return this.Seek(0L, SeekOrigin.Current);
			}
		}
		return this.ᜂ.Position;
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00038620 File Offset: 0x00037620
	public virtual void ᜀ(long A_0)
	{
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_70;
			case 2:
				goto IL_5A;
			}
			if (!this.ᜃ)
			{
				break;
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
				num = 2;
				continue;
			}
			IL_5A:
			this.ᜂ.Position = A_0;
			num = 0;
		}
		IL_70:
		this.Seek(A_0, SeekOrigin.Begin);
	}

	// Token: 0x0400058F RID: 1423
	private new sprᢗ ᜀ;

	// Token: 0x04000590 RID: 1424
	private new UCOMIStream ᜁ;

	// Token: 0x04000591 RID: 1425
	private Stream ᜂ;

	// Token: 0x04000592 RID: 1426
	private bool ᜃ;
}
