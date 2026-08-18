using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000FB1 RID: 4017
	[Serializable]
	public class DockState : RadControlState
	{
		// Token: 0x170030C7 RID: 12487
		// (get) Token: 0x06009A2B RID: 39467 RVA: 0x00225CD5 File Offset: 0x00223ED5
		// (set) Token: 0x06009A2C RID: 39468 RVA: 0x00225CE7 File Offset: 0x00223EE7
		public string DockZoneID
		{
			get
			{
				return this.GetValue("DockZoneID", string.Empty);
			}
			set
			{
				this._values["DockZoneID"] = value;
			}
		}

		// Token: 0x170030C8 RID: 12488
		// (get) Token: 0x06009A2D RID: 39469 RVA: 0x00225CFA File Offset: 0x00223EFA
		// (set) Token: 0x06009A2E RID: 39470 RVA: 0x00225D0C File Offset: 0x00223F0C
		public Unit Width
		{
			get
			{
				return this.GetValue("Width", Unit.Empty);
			}
			set
			{
				this._values["Width"] = value.ToString();
			}
		}

		// Token: 0x170030C9 RID: 12489
		// (get) Token: 0x06009A2F RID: 39471 RVA: 0x00225D2B File Offset: 0x00223F2B
		// (set) Token: 0x06009A30 RID: 39472 RVA: 0x00225D39 File Offset: 0x00223F39
		public int ExpandedHeight
		{
			get
			{
				return this.GetValue("ExpandedHeight", 0);
			}
			set
			{
				this._values["ExpandedHeight"] = value.ToString();
			}
		}

		// Token: 0x170030CA RID: 12490
		// (get) Token: 0x06009A31 RID: 39473 RVA: 0x00225D52 File Offset: 0x00223F52
		// (set) Token: 0x06009A32 RID: 39474 RVA: 0x00225D64 File Offset: 0x00223F64
		public Unit Height
		{
			get
			{
				return this.GetValue("Height", Unit.Empty);
			}
			set
			{
				this._values["Height"] = value.ToString();
			}
		}

		// Token: 0x170030CB RID: 12491
		// (get) Token: 0x06009A33 RID: 39475 RVA: 0x00225D83 File Offset: 0x00223F83
		// (set) Token: 0x06009A34 RID: 39476 RVA: 0x00225D91 File Offset: 0x00223F91
		public int Index
		{
			get
			{
				return this.GetValue("Index", 0);
			}
			set
			{
				this._values["Index"] = value.ToString();
			}
		}

		// Token: 0x170030CC RID: 12492
		// (get) Token: 0x06009A35 RID: 39477 RVA: 0x00225DAA File Offset: 0x00223FAA
		// (set) Token: 0x06009A36 RID: 39478 RVA: 0x00225DBC File Offset: 0x00223FBC
		public Unit Top
		{
			get
			{
				return this.GetValue("Top", Unit.Empty);
			}
			set
			{
				this._values["Top"] = value.ToString();
			}
		}

		// Token: 0x170030CD RID: 12493
		// (get) Token: 0x06009A37 RID: 39479 RVA: 0x00225DDB File Offset: 0x00223FDB
		// (set) Token: 0x06009A38 RID: 39480 RVA: 0x00225DED File Offset: 0x00223FED
		public Unit Left
		{
			get
			{
				return this.GetValue("Left", Unit.Empty);
			}
			set
			{
				this._values["Left"] = value.ToString();
			}
		}

		// Token: 0x170030CE RID: 12494
		// (get) Token: 0x06009A39 RID: 39481 RVA: 0x00225E0C File Offset: 0x0022400C
		// (set) Token: 0x06009A3A RID: 39482 RVA: 0x00225E1A File Offset: 0x0022401A
		public bool Closed
		{
			get
			{
				return this.GetValue("Closed", false);
			}
			set
			{
				this._values["Closed"] = value.ToString();
			}
		}

		// Token: 0x170030CF RID: 12495
		// (get) Token: 0x06009A3B RID: 39483 RVA: 0x00225E33 File Offset: 0x00224033
		// (set) Token: 0x06009A3C RID: 39484 RVA: 0x00225E41 File Offset: 0x00224041
		public bool Resizable
		{
			get
			{
				return this.GetValue("Resizable", false);
			}
			set
			{
				this._values["Resizable"] = value.ToString();
			}
		}

		// Token: 0x170030D0 RID: 12496
		// (get) Token: 0x06009A3D RID: 39485 RVA: 0x00225E5A File Offset: 0x0022405A
		// (set) Token: 0x06009A3E RID: 39486 RVA: 0x00225E68 File Offset: 0x00224068
		public bool Collapsed
		{
			get
			{
				return this.GetValue("Collapsed", false);
			}
			set
			{
				this._values["Collapsed"] = value.ToString();
			}
		}

		// Token: 0x170030D1 RID: 12497
		// (get) Token: 0x06009A3F RID: 39487 RVA: 0x00225E81 File Offset: 0x00224081
		// (set) Token: 0x06009A40 RID: 39488 RVA: 0x00225E8F File Offset: 0x0022408F
		public bool Pinned
		{
			get
			{
				return this.GetValue("Pinned", false);
			}
			set
			{
				this._values["Pinned"] = value.ToString();
			}
		}

		// Token: 0x170030D2 RID: 12498
		// (get) Token: 0x06009A41 RID: 39489 RVA: 0x00225EA8 File Offset: 0x002240A8
		// (set) Token: 0x06009A42 RID: 39490 RVA: 0x00225EBA File Offset: 0x002240BA
		public string UniqueName
		{
			get
			{
				return this.GetValue("UniqueName", string.Empty);
			}
			set
			{
				this._values["UniqueName"] = value;
			}
		}

		// Token: 0x170030D3 RID: 12499
		// (get) Token: 0x06009A43 RID: 39491 RVA: 0x00225ECD File Offset: 0x002240CD
		// (set) Token: 0x06009A44 RID: 39492 RVA: 0x00225EDF File Offset: 0x002240DF
		public string Tag
		{
			get
			{
				return this.GetValue("Tag", string.Empty);
			}
			set
			{
				this._values["Tag"] = value;
			}
		}

		// Token: 0x170030D4 RID: 12500
		// (get) Token: 0x06009A45 RID: 39493 RVA: 0x00225EF2 File Offset: 0x002240F2
		// (set) Token: 0x06009A46 RID: 39494 RVA: 0x00225F04 File Offset: 0x00224104
		public string Title
		{
			get
			{
				return this.GetValue("Title", string.Empty);
			}
			set
			{
				this._values["Title"] = value;
			}
		}

		// Token: 0x170030D5 RID: 12501
		// (get) Token: 0x06009A47 RID: 39495 RVA: 0x00225F17 File Offset: 0x00224117
		// (set) Token: 0x06009A48 RID: 39496 RVA: 0x00225F29 File Offset: 0x00224129
		public string Text
		{
			get
			{
				return this.GetValue("Text", string.Empty);
			}
			set
			{
				this._values["Text"] = value;
			}
		}

		// Token: 0x06009A49 RID: 39497 RVA: 0x00225F3C File Offset: 0x0022413C
		private Unit GetValue(string key, Unit defaultValue)
		{
			if (this._values.ContainsKey(key))
			{
				return Unit.Parse(this._values[key]);
			}
			return defaultValue;
		}

		// Token: 0x06009A4A RID: 39498 RVA: 0x00225F5F File Offset: 0x0022415F
		private int GetValue(string key, int defaultValue)
		{
			if (this._values.ContainsKey(key))
			{
				return int.Parse(this._values[key]);
			}
			return defaultValue;
		}

		// Token: 0x06009A4B RID: 39499 RVA: 0x00225F82 File Offset: 0x00224182
		private bool GetValue(string key, bool defaultValue)
		{
			if (this._values.ContainsKey(key))
			{
				return bool.Parse(this._values[key]);
			}
			return defaultValue;
		}

		// Token: 0x06009A4C RID: 39500 RVA: 0x00225FA5 File Offset: 0x002241A5
		private string GetValue(string key, string defaultValue)
		{
			if (this._values.ContainsKey(key))
			{
				return this._values[key];
			}
			return defaultValue;
		}

		// Token: 0x06009A4D RID: 39501 RVA: 0x00225FC4 File Offset: 0x002241C4
		public override string ToString()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(this._values);
		}

		// Token: 0x06009A4E RID: 39502 RVA: 0x00225FE4 File Offset: 0x002241E4
		public static DockState Deserialize(string input)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return new DockState(javaScriptSerializer.Deserialize<Dictionary<string, string>>(input));
		}

		// Token: 0x06009A4F RID: 39503 RVA: 0x00226003 File Offset: 0x00224203
		public DockState() : this(new Dictionary<string, string>())
		{
		}

		// Token: 0x06009A50 RID: 39504 RVA: 0x00226010 File Offset: 0x00224210
		private DockState(Dictionary<string, string> values)
		{
			this._values = ((values != null) ? values : new Dictionary<string, string>());
		}

		// Token: 0x04002BBE RID: 11198
		private readonly Dictionary<string, string> _values;
	}
}
