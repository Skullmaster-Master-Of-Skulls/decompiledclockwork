using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001961 RID: 6497
	public class RadDataPagerCommandEventArgs : CommandEventArgs
	{
		// Token: 0x17004C02 RID: 19458
		// (get) Token: 0x0600FB8E RID: 64398 RVA: 0x0038B4AE File Offset: 0x003896AE
		public RadDataPager DataPager
		{
			get
			{
				return this._dataPager;
			}
		}

		// Token: 0x17004C03 RID: 19459
		// (get) Token: 0x0600FB8F RID: 64399 RVA: 0x0038B4B6 File Offset: 0x003896B6
		public RadDataPagerFieldItem DataPagerItem
		{
			get
			{
				return this._dataPagerItem;
			}
		}

		// Token: 0x17004C04 RID: 19460
		// (get) Token: 0x0600FB90 RID: 64400 RVA: 0x0038B4BE File Offset: 0x003896BE
		public object EventSource
		{
			get
			{
				return this._eventSource;
			}
		}

		// Token: 0x0600FB91 RID: 64401 RVA: 0x0038B4C6 File Offset: 0x003896C6
		public RadDataPagerCommandEventArgs(RadDataPager dataPager, RadDataPagerFieldItem dataPagerItem, object eventSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._dataPager = dataPager;
			this._dataPagerItem = dataPagerItem;
			this._eventSource = eventSource;
		}

		// Token: 0x0600FB92 RID: 64402 RVA: 0x0038B4E5 File Offset: 0x003896E5
		public RadDataPagerCommandEventArgs(RadDataPager dataPager, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._dataPager = dataPager;
		}

		// Token: 0x0600FB93 RID: 64403 RVA: 0x0038B4F5 File Offset: 0x003896F5
		public RadDataPagerCommandEventArgs(RadDataPager dataPager, string commandName, object commandArgs) : base(commandName, commandArgs)
		{
			this._dataPager = dataPager;
		}

		// Token: 0x0400478E RID: 18318
		private RadDataPager _dataPager;

		// Token: 0x0400478F RID: 18319
		private RadDataPagerFieldItem _dataPagerItem;

		// Token: 0x04004790 RID: 18320
		private object _eventSource;
	}
}
