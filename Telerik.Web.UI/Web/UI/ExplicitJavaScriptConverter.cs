using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Common.SerializeJS;

namespace Telerik.Web.UI
{
	// Token: 0x0200000C RID: 12
	public abstract class ExplicitJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x000035C3 File Offset: 0x000017C3
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000035D0 File Offset: 0x000017D0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			this.Markers = new JavaScriptSerializerMarkers();
			this.PopulateProperties(dictionary, obj);
			return dictionary;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000035F7 File Offset: 0x000017F7
		protected virtual void PopulateProperties(IDictionary<string, object> state, object obj)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000035F9 File Offset: 0x000017F9
		protected static void AddProperty(IDictionary<string, object> state, string key, object value, object defaultValue)
		{
			if (!ExplicitJavaScriptConverter.IsDefaultValue(value, defaultValue))
			{
				ExplicitJavaScriptConverter.AddProperty(state, key, value);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000360C File Offset: 0x0000180C
		protected static void AddProperty(IDictionary<string, object> state, string key, object value)
		{
			if (!ExplicitJavaScriptConverter.IsDefaultObject(value))
			{
				state[key] = value;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000361E File Offset: 0x0000181E
		private static bool IsDefaultValue(object value, object defaultValue)
		{
			return (value == null && defaultValue == null) || value.Equals(defaultValue);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003630 File Offset: 0x00001830
		private static bool IsDefaultObject(object value)
		{
			IDefaultCheck defaultCheck = value as IDefaultCheck;
			return defaultCheck != null && defaultCheck.IsDefault;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003650 File Offset: 0x00001850
		protected void AddScript(IDictionary<string, object> state, string key, object value)
		{
			string text = value.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				state[key] = this.Markers.WrapInMarkers(text);
			}
		}

		// Token: 0x0400000F RID: 15
		internal JavaScriptSerializerMarkers Markers;
	}
}
