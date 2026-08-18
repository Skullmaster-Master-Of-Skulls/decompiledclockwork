using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000165 RID: 357
	public class RatingEventArgs : EventArgs
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x0001894C File Offset: 0x00016B4C
		public RatingEventArgs(string args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			string[] array = args.Split(new char[]
			{
				';'
			});
			if (array.Length == 2)
			{
				this._value = array[0];
				this._tag = array[1];
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00018999 File Offset: 0x00016B99
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x000189A1 File Offset: 0x00016BA1
		public string Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000189A9 File Offset: 0x00016BA9
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x000189B1 File Offset: 0x00016BB1
		public string CallbackResult
		{
			get
			{
				return this._callbackResult;
			}
			set
			{
				this._callbackResult = value;
			}
		}

		// Token: 0x040003BE RID: 958
		private string _value;

		// Token: 0x040003BF RID: 959
		private string _tag;

		// Token: 0x040003C0 RID: 960
		private string _callbackResult;
	}
}
