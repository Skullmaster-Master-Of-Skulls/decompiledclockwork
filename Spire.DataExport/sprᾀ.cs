using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.XLS.IO;

// Token: 0x02000004 RID: 4
internal class sprᾀ : sprḗ
{
	// Token: 0x0600000A RID: 10 RVA: 0x000034E4 File Offset: 0x000024E4
	public sprᾀ(string A_0)
	{
		int a_ = 0;
		base..ctor();
		OLE_MODE a_2 = OLE_MODE.STGM_READWRITE | OLE_MODE.STGM_SHARE_EXCLUSIVE;
		SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
		sprᢗ sprᢗ;
		try
		{
			securityPermission.Assert();
			sprᾀ.ᜀ(A_0, null, (int)a_2, (IntPtr)0, 0, out sprᢗ);
			goto IL_39;
		}
		catch
		{
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("唛瀝嘟䌡䠣伥䰧攩尫䬭䈯匱䀳張圷吹挻欽⸿⽁╃⡅⥇ⵉ⥋⩍ፏ㵑こ㍕", a_)));
		}
		goto IL_A8;
		IL_39:
		UCOMIStream ucomistream;
		if (sprᢗ != null)
		{
			ucomistream = null;
			a_2 = (OLE_MODE.STGM_READWRITE | OLE_MODE.STGM_SHARE_EXCLUSIVE);
			try
			{
				sprᢗ.ᜀ(HyperlinksCollectionEditor.b("䬛焝刟䤡䘣䤥䜧䄩", a_), (IntPtr)0, (uint)a_2, 0U, out ucomistream);
				goto IL_64;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + HyperlinksCollectionEditor.b("ᄛᐝ爟䜡䔣䈥朧䘩䤫紭䐯䀱儳圵唷9ػ氽┿⍁⁃ॅ⑇⽉Ὃ㩍≏㝑㕓㭕瑗ⱙ㵛ⱝ婟", a_));
			}
		}
		throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("唛瀝嘟䌡䠣伥䰧攩尫䬭䈯匱䀳張圷吹挻焽〿❁⩃ཅᭇ㹉⍋㱍ㅏ㕑ㅓၕㅗ㙙㥛", a_)), A_0));
		IL_64:
		if (ucomistream == null)
		{
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("唛瀝嘟䌡䠣伥䰧攩尫䬭䈯匱䀳張圷吹挻焽〿❁⩃ᕅ㱇㡉⥋⽍㵏", a_)));
		}
		IL_A8:
		STATSTG statstg;
		ucomistream.Stat(out statstg, 1);
		byte[] array = new byte[statstg.cbSize];
		GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
		try
		{
			ucomistream.Read(array, (int)statstg.cbSize, (IntPtr)0);
			goto IL_139;
		}
		finally
		{
			gchandle.Free();
		}
		goto IL_64;
		IL_139:
		this.ᜀ = new MemoryStream(array);
	}

	// Token: 0x0600000B RID: 11
	[DllImport("ole32", CharSet = CharSet.Unicode)]
	private static extern void StgOpenStorage(string A_0, sprᢗ A_1, int A_2, IntPtr A_3, int A_4, out sprᢗ A_5);

	// Token: 0x0600000C RID: 12 RVA: 0x00003660 File Offset: 0x00002660
	private static void ᜀ(string A_0, sprᢗ A_1, int A_2, IntPtr A_3, int A_4, out sprᢗ A_5)
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
		sprᾀ.StgOpenStorage(A_0, A_1, A_2, A_3, A_4, out A_5);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000036A8 File Offset: 0x000026A8
	public virtual void ᜆ()
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
		this.ᜀ.Close();
		base.Close();
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000036F4 File Offset: 0x000026F4
	public virtual int ᜀ(byte[] A_0, int A_1, int A_2)
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
		return this.ᜀ.Read(A_0, A_1, A_2);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00003740 File Offset: 0x00002740
	public override int ᜀ(byte[] A_0, int A_1)
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
		return this.ᜀ.Read(A_0, 0, A_1);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000378C File Offset: 0x0000278C
	public virtual void ᜁ(byte[] A_0, int A_1, int A_2)
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
		this.ᜀ.Write(A_0, A_1, A_2);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000037D8 File Offset: 0x000027D8
	public override int ᜁ(byte[] A_0, int A_1)
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
		this.ᜀ.Write(A_0, 0, A_1);
		return 0;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00003824 File Offset: 0x00002824
	public override long ᜀ(long A_0, SeekOrigin A_1)
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
		return this.ᜀ.Seek(A_0, A_1);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000386C File Offset: 0x0000286C
	public virtual void ᜁ(long A_0)
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
		this.ᜀ.SetLength(A_0);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000038B4 File Offset: 0x000028B4
	public virtual void ᜄ()
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
		this.ᜀ.Flush();
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000038FC File Offset: 0x000028FC
	public virtual bool ᜂ()
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
		return true;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00003938 File Offset: 0x00002938
	public virtual bool ᜀ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00003974 File Offset: 0x00002974
	public virtual bool ᜁ()
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
		return true;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000039B0 File Offset: 0x000029B0
	public virtual long ᜅ()
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
		return this.ᜀ.Length;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000039F8 File Offset: 0x000029F8
	public virtual long ᜃ()
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
		return this.ᜀ.Position;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00003A40 File Offset: 0x00002A40
	public virtual void ᜀ(long A_0)
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
		this.ᜀ.Position = A_0;
	}

	// Token: 0x04000004 RID: 4
	private new MemoryStream ᜀ;
}
