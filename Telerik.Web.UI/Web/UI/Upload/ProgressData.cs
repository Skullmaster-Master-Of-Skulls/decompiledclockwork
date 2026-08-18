using System;
using System.Collections.Generic;
using System.IO;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001347 RID: 4935
	public class ProgressData
	{
		// Token: 0x1700421D RID: 16925
		public object this[string key]
		{
			get
			{
				lock (this.progressLock)
				{
					if (this._items.ContainsKey(key))
					{
						return this._items[key];
					}
				}
				return null;
			}
			set
			{
				lock (this.progressLock)
				{
					this._items[key] = value;
				}
			}
		}

		// Token: 0x1700421E RID: 16926
		// (get) Token: 0x0600CDB6 RID: 52662 RVA: 0x002DCA58 File Offset: 0x002DAC58
		// (set) Token: 0x0600CDB7 RID: 52663 RVA: 0x002DCA65 File Offset: 0x002DAC65
		public virtual object PrimaryTotal
		{
			get
			{
				return this["PrimaryTotal"];
			}
			set
			{
				this["PrimaryTotal"] = value;
			}
		}

		// Token: 0x1700421F RID: 16927
		// (get) Token: 0x0600CDB8 RID: 52664 RVA: 0x002DCA73 File Offset: 0x002DAC73
		// (set) Token: 0x0600CDB9 RID: 52665 RVA: 0x002DCA80 File Offset: 0x002DAC80
		public virtual object PrimaryValue
		{
			get
			{
				return this["PrimaryValue"];
			}
			set
			{
				this["PrimaryValue"] = value;
			}
		}

		// Token: 0x17004220 RID: 16928
		// (get) Token: 0x0600CDBA RID: 52666 RVA: 0x002DCA8E File Offset: 0x002DAC8E
		// (set) Token: 0x0600CDBB RID: 52667 RVA: 0x002DCA9B File Offset: 0x002DAC9B
		public virtual object PrimaryPercent
		{
			get
			{
				return this["PrimaryPercent"];
			}
			set
			{
				this["PrimaryPercent"] = value;
			}
		}

		// Token: 0x17004221 RID: 16929
		// (get) Token: 0x0600CDBC RID: 52668 RVA: 0x002DCAA9 File Offset: 0x002DACA9
		// (set) Token: 0x0600CDBD RID: 52669 RVA: 0x002DCAB6 File Offset: 0x002DACB6
		public virtual object SecondaryTotal
		{
			get
			{
				return this["SecondaryTotal"];
			}
			set
			{
				this["SecondaryTotal"] = value;
			}
		}

		// Token: 0x17004222 RID: 16930
		// (get) Token: 0x0600CDBE RID: 52670 RVA: 0x002DCAC4 File Offset: 0x002DACC4
		// (set) Token: 0x0600CDBF RID: 52671 RVA: 0x002DCAD1 File Offset: 0x002DACD1
		public virtual object SecondaryValue
		{
			get
			{
				return this["SecondaryValue"];
			}
			set
			{
				this["SecondaryValue"] = value;
			}
		}

		// Token: 0x17004223 RID: 16931
		// (get) Token: 0x0600CDC0 RID: 52672 RVA: 0x002DCADF File Offset: 0x002DACDF
		// (set) Token: 0x0600CDC1 RID: 52673 RVA: 0x002DCAEC File Offset: 0x002DACEC
		public virtual object SecondaryPercent
		{
			get
			{
				return this["SecondaryPercent"];
			}
			set
			{
				this["SecondaryPercent"] = value;
			}
		}

		// Token: 0x17004224 RID: 16932
		// (get) Token: 0x0600CDC2 RID: 52674 RVA: 0x002DCAFA File Offset: 0x002DACFA
		// (set) Token: 0x0600CDC3 RID: 52675 RVA: 0x002DCB07 File Offset: 0x002DAD07
		public virtual object CurrentOperationText
		{
			get
			{
				return this["CurrentOperationText"];
			}
			set
			{
				this["CurrentOperationText"] = value;
			}
		}

		// Token: 0x17004225 RID: 16933
		// (get) Token: 0x0600CDC4 RID: 52676 RVA: 0x002DCB15 File Offset: 0x002DAD15
		// (set) Token: 0x0600CDC5 RID: 52677 RVA: 0x002DCB22 File Offset: 0x002DAD22
		public virtual object Speed
		{
			get
			{
				return this["Speed"];
			}
			set
			{
				this["Speed"] = value;
			}
		}

		// Token: 0x17004226 RID: 16934
		// (get) Token: 0x0600CDC6 RID: 52678 RVA: 0x002DCB30 File Offset: 0x002DAD30
		// (set) Token: 0x0600CDC7 RID: 52679 RVA: 0x002DCB3D File Offset: 0x002DAD3D
		public virtual object TimeEstimated
		{
			get
			{
				return this["TimeEstimated"];
			}
			set
			{
				this["TimeEstimated"] = value;
			}
		}

		// Token: 0x17004227 RID: 16935
		// (get) Token: 0x0600CDC8 RID: 52680 RVA: 0x002DCB4B File Offset: 0x002DAD4B
		// (set) Token: 0x0600CDC9 RID: 52681 RVA: 0x002DCB58 File Offset: 0x002DAD58
		public virtual object TimeElapsed
		{
			get
			{
				return this["TimeElapsed"];
			}
			set
			{
				this["TimeElapsed"] = value;
			}
		}

		// Token: 0x17004228 RID: 16936
		// (get) Token: 0x0600CDCA RID: 52682 RVA: 0x002DCB66 File Offset: 0x002DAD66
		// (set) Token: 0x0600CDCB RID: 52683 RVA: 0x002DCB82 File Offset: 0x002DAD82
		public virtual bool OperationComplete
		{
			get
			{
				return (bool)(this["OperationComplete"] ?? false);
			}
			set
			{
				this["OperationComplete"] = value;
			}
		}

		// Token: 0x0600CDCC RID: 52684 RVA: 0x002DCB95 File Offset: 0x002DAD95
		protected virtual void SerializeCustomData(TextWriter writer)
		{
		}

		// Token: 0x0600CDCD RID: 52685 RVA: 0x002DCB98 File Offset: 0x002DAD98
		public virtual void Serialize(TextWriter writer)
		{
			writer.Write("var rawProgressData = {");
			if (this._items.Keys.Count > 0)
			{
				writer.Write("InProgress:true");
			}
			else
			{
				writer.Write("InProgress:false");
			}
			writer.Write(",ProgressCounters:{0}", (this._items.Keys.Count > 0).ToString().ToLower());
			lock (this.progressLock)
			{
				foreach (string text in this._items.Keys)
				{
					writer.Write(",");
					writer.Write(text);
					writer.Write(":'");
					this.WriteValue(writer, text);
					writer.Write("'");
				}
				this.SerializeCustomData(writer);
			}
			writer.Write("};");
			if (this._items.ContainsKey("OperationComplete") && this._items["OperationComplete"].ToString() == "True")
			{
				this._items.Clear();
			}
		}

		// Token: 0x0600CDCE RID: 52686 RVA: 0x002DCCF4 File Offset: 0x002DAEF4
		private void WriteValue(TextWriter writer, string key)
		{
			object obj = this._items[key];
			if (obj is bool)
			{
				writer.Write(obj.ToString().ToLower());
				return;
			}
			if (obj is int)
			{
				writer.Write(obj);
				return;
			}
			writer.Write(this.FormatString(obj.ToString()));
		}

		// Token: 0x0600CDCF RID: 52687 RVA: 0x002DCD4A File Offset: 0x002DAF4A
		protected string FormatString(string formatee)
		{
			return formatee.Replace("\\", "\\\\").Replace("'", "\\'");
		}

		// Token: 0x040036FE RID: 14078
		private const string PrimaryTotalKey = "PrimaryTotal";

		// Token: 0x040036FF RID: 14079
		private const string PrimaryValueKey = "PrimaryValue";

		// Token: 0x04003700 RID: 14080
		private const string PrimaryPercentKey = "PrimaryPercent";

		// Token: 0x04003701 RID: 14081
		private const string SecondaryTotalKey = "SecondaryTotal";

		// Token: 0x04003702 RID: 14082
		private const string SecondaryValueKey = "SecondaryValue";

		// Token: 0x04003703 RID: 14083
		private const string SecondaryPercentKey = "SecondaryPercent";

		// Token: 0x04003704 RID: 14084
		private const string CurrentOperationTextKey = "CurrentOperationText";

		// Token: 0x04003705 RID: 14085
		private const string SpeedKey = "Speed";

		// Token: 0x04003706 RID: 14086
		private const string TimeEstimatedKey = "TimeEstimated";

		// Token: 0x04003707 RID: 14087
		private const string TimeElapsedKey = "TimeElapsed";

		// Token: 0x04003708 RID: 14088
		private const string OperationCompleteKey = "OperationComplete";

		// Token: 0x04003709 RID: 14089
		private Dictionary<string, object> _items = new Dictionary<string, object>();

		// Token: 0x0400370A RID: 14090
		private object progressLock = new object();
	}
}
