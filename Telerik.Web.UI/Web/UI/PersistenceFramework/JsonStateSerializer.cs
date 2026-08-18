using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000498 RID: 1176
	public class JsonStateSerializer : IStateSerializer
	{
		// Token: 0x060029D4 RID: 10708 RVA: 0x00086B66 File Offset: 0x00084D66
		public JsonStateSerializer()
		{
			this._jss = new JavaScriptSerializer();
			this.initializeConverters();
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00086B7F File Offset: 0x00084D7F
		public string Serialize(RadControlState state)
		{
			return this._jss.Serialize(state);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00086B8D File Offset: 0x00084D8D
		public RadControlState Deserialize(string stateData)
		{
			return this.Deserialize<RadControlState>(stateData);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00086B96 File Offset: 0x00084D96
		public string Serialize(List<RadControlState> stateCollection)
		{
			return this._jss.Serialize(stateCollection);
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x00086BA4 File Offset: 0x00084DA4
		public List<RadControlState> DeserializeCollection(string stateData)
		{
			return this.Deserialize<List<RadControlState>>(stateData);
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x00086BAD File Offset: 0x00084DAD
		public List<JavaScriptConverter> Converters
		{
			get
			{
				if (this._converters == null)
				{
					this.initializeConverters();
				}
				return this._converters;
			}
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x00086BC3 File Offset: 0x00084DC3
		public void AddConverter(JavaScriptConverter converter)
		{
			if (converter == null)
			{
				throw new PersistenceFrameworkArgumentNullException();
			}
			this._converters.Add(converter);
			this._jss.RegisterConverters(this._converters);
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x00086BEB File Offset: 0x00084DEB
		public T Deserialize<T>(string stateData)
		{
			return this._jss.Deserialize<T>(stateData);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x00086BFC File Offset: 0x00084DFC
		private void initializeConverters()
		{
			this._converters = new List<JavaScriptConverter>();
			this._converters.Add(new UnitConverter());
			this._converters.Add(new ControlSettingConverter());
			this._converters.Add(new ColorConverter());
			this._jss.RegisterConverters(this._converters);
		}

		// Token: 0x04000ABB RID: 2747
		private JavaScriptSerializer _jss;

		// Token: 0x04000ABC RID: 2748
		private List<JavaScriptConverter> _converters;
	}
}
