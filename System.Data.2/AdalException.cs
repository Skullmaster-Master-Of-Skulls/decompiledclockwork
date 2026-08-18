using System;

// Token: 0x0200000C RID: 12
internal class AdalException : Exception
{
	// Token: 0x06000096 RID: 150 RVA: 0x000043B8 File Offset: 0x000037B8
	internal AdalException(string message, uint category, uint status, uint state) : base(message)
	{
		this._category = category;
		this._status = status;
		this._state = state;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000043E4 File Offset: 0x000037E4
	internal uint GetCategory()
	{
		return this._category;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x000043F8 File Offset: 0x000037F8
	internal uint GetStatus()
	{
		return this._status;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000440C File Offset: 0x0000380C
	internal uint GetState()
	{
		return this._state;
	}

	// Token: 0x04000090 RID: 144
	private readonly uint _category;

	// Token: 0x04000091 RID: 145
	private readonly uint _status;

	// Token: 0x04000092 RID: 146
	private readonly uint _state;
}
