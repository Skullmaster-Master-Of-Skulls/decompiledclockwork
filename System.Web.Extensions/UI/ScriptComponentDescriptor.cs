using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Resources;
using System.Web.Script.Serialization;

namespace System.Web.UI
{
	// Token: 0x0200006E RID: 110
	public class ScriptComponentDescriptor : ScriptDescriptor
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x00014037 File Offset: 0x00012237
		public ScriptComponentDescriptor(string type)
		{
			if (string.IsNullOrEmpty(type))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "type");
			}
			this._type = type;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00014065 File Offset: 0x00012265
		internal ScriptComponentDescriptor(string type, string elementID) : this(type)
		{
			if (string.IsNullOrEmpty(elementID))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "elementID");
			}
			this._elementIDInternal = elementID;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0001408D File Offset: 0x0001228D
		public virtual string ClientID
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00014095 File Offset: 0x00012295
		internal string ElementIDInternal
		{
			get
			{
				return this._elementIDInternal;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001409D File Offset: 0x0001229D
		private SortedList<string, string> Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new SortedList<string, string>(StringComparer.Ordinal);
				}
				return this._events;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000140BD File Offset: 0x000122BD
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x000140CE File Offset: 0x000122CE
		public virtual string ID
		{
			get
			{
				return this._id ?? string.Empty;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000140D7 File Offset: 0x000122D7
		private SortedList<string, ScriptComponentDescriptor.Expression> Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new SortedList<string, ScriptComponentDescriptor.Expression>(StringComparer.Ordinal);
				}
				return this._properties;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000140F7 File Offset: 0x000122F7
		// (set) Token: 0x060003DB RID: 987 RVA: 0x000140FF File Offset: 0x000122FF
		internal bool RegisterDispose
		{
			get
			{
				return this._registerDispose;
			}
			set
			{
				this._registerDispose = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00014108 File Offset: 0x00012308
		private JavaScriptSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this._serializer = new JavaScriptSerializer();
				}
				return this._serializer;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00014123 File Offset: 0x00012323
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0001412B File Offset: 0x0001232B
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "value");
				}
				this._type = value;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001414C File Offset: 0x0001234C
		public void AddComponentProperty(string name, string componentID)
		{
			if (string.IsNullOrEmpty(componentID))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "componentID");
			}
			this.AddProperty(name, new ScriptComponentDescriptor.ComponentReference(componentID));
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00014173 File Offset: 0x00012373
		public void AddElementProperty(string name, string elementID)
		{
			if (string.IsNullOrEmpty(elementID))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "elementID");
			}
			this.AddProperty(name, new ScriptComponentDescriptor.ElementReference(elementID));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001419A File Offset: 0x0001239A
		public void AddEvent(string name, string handler)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "name");
			}
			if (string.IsNullOrEmpty(handler))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "handler");
			}
			this.Events[name] = handler;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000141D9 File Offset: 0x000123D9
		public void AddProperty(string name, object value)
		{
			this.AddProperty(name, new ScriptComponentDescriptor.ObjectReference(value));
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000141E8 File Offset: 0x000123E8
		private void AddProperty(string name, ScriptComponentDescriptor.Expression value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "name");
			}
			this.Properties[name] = value;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001420F File Offset: 0x0001240F
		public void AddScriptProperty(string name, string script)
		{
			if (string.IsNullOrEmpty(script))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "script");
			}
			this.AddProperty(name, new ScriptComponentDescriptor.ScriptExpression(script));
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00014238 File Offset: 0x00012438
		private void AppendEventsScript(StringBuilder builder)
		{
			if (this._events != null && this._events.Count > 0)
			{
				builder.Append('{');
				bool flag = true;
				foreach (KeyValuePair<string, string> keyValuePair in this._events)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						builder.Append(',');
					}
					builder.Append('"');
					builder.Append(HttpUtility.JavaScriptStringEncode(keyValuePair.Key));
					builder.Append('"');
					builder.Append(':');
					builder.Append(keyValuePair.Value);
				}
				builder.Append("}");
				return;
			}
			builder.Append("null");
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001430C File Offset: 0x0001250C
		private void AppendPropertiesScript(StringBuilder builder)
		{
			bool flag = true;
			if (this._properties != null && this._properties.Count > 0)
			{
				foreach (KeyValuePair<string, ScriptComponentDescriptor.Expression> keyValuePair in this._properties)
				{
					if (keyValuePair.Value.Type == ScriptComponentDescriptor.ExpressionType.Script)
					{
						if (flag)
						{
							builder.Append("{");
							flag = false;
						}
						else
						{
							builder.Append(",");
						}
						builder.Append('"');
						builder.Append(HttpUtility.JavaScriptStringEncode(keyValuePair.Key));
						builder.Append('"');
						builder.Append(':');
						keyValuePair.Value.AppendValue(this.Serializer, builder);
					}
				}
			}
			if (flag)
			{
				builder.Append("null");
				return;
			}
			builder.Append("}");
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000143FC File Offset: 0x000125FC
		private void AppendReferencesScript(StringBuilder builder)
		{
			bool flag = true;
			if (this._properties != null && this._properties.Count > 0)
			{
				foreach (KeyValuePair<string, ScriptComponentDescriptor.Expression> keyValuePair in this._properties)
				{
					if (keyValuePair.Value.Type == ScriptComponentDescriptor.ExpressionType.ComponentReference)
					{
						if (flag)
						{
							builder.Append("{");
							flag = false;
						}
						else
						{
							builder.Append(",");
						}
						builder.Append('"');
						builder.Append(HttpUtility.JavaScriptStringEncode(keyValuePair.Key));
						builder.Append('"');
						builder.Append(':');
						keyValuePair.Value.AppendValue(this.Serializer, builder);
					}
				}
			}
			if (flag)
			{
				builder.Append("null");
				return;
			}
			builder.Append("}");
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000144EC File Offset: 0x000126EC
		protected internal override string GetScript()
		{
			if (!string.IsNullOrEmpty(this.ID))
			{
				this.AddProperty("id", this.ID);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("$create(");
			stringBuilder.Append(this.Type);
			stringBuilder.Append(", ");
			this.AppendPropertiesScript(stringBuilder);
			stringBuilder.Append(", ");
			this.AppendEventsScript(stringBuilder);
			stringBuilder.Append(", ");
			this.AppendReferencesScript(stringBuilder);
			if (this.ElementIDInternal != null)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append("$get(\"");
				stringBuilder.Append(HttpUtility.JavaScriptStringEncode(this.ElementIDInternal));
				stringBuilder.Append("\")");
			}
			stringBuilder.Append(");");
			return stringBuilder.ToString();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000145BF File Offset: 0x000127BF
		internal override void RegisterDisposeForDescriptor(ScriptManager scriptManager, Control owner)
		{
			if (this.RegisterDispose && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterDispose(owner, "$find('" + this.ID + "').dispose();");
			}
		}

		// Token: 0x04000175 RID: 373
		private string _elementIDInternal;

		// Token: 0x04000176 RID: 374
		private SortedList<string, string> _events;

		// Token: 0x04000177 RID: 375
		private string _id;

		// Token: 0x04000178 RID: 376
		private SortedList<string, ScriptComponentDescriptor.Expression> _properties;

		// Token: 0x04000179 RID: 377
		private bool _registerDispose = true;

		// Token: 0x0400017A RID: 378
		private JavaScriptSerializer _serializer;

		// Token: 0x0400017B RID: 379
		private string _type;

		// Token: 0x0200015D RID: 349
		private abstract class Expression
		{
			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06000FFB RID: 4091
			public abstract ScriptComponentDescriptor.ExpressionType Type { get; }

			// Token: 0x06000FFC RID: 4092
			public abstract void AppendValue(JavaScriptSerializer serializer, StringBuilder builder);
		}

		// Token: 0x0200015E RID: 350
		private enum ExpressionType
		{
			// Token: 0x040004D7 RID: 1239
			Script,
			// Token: 0x040004D8 RID: 1240
			ComponentReference
		}

		// Token: 0x0200015F RID: 351
		private sealed class ComponentReference : ScriptComponentDescriptor.Expression
		{
			// Token: 0x06000FFE RID: 4094 RVA: 0x00037720 File Offset: 0x00035920
			public ComponentReference(string componentID)
			{
				this._componentID = componentID;
			}

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0001D1CA File Offset: 0x0001B3CA
			public override ScriptComponentDescriptor.ExpressionType Type
			{
				get
				{
					return ScriptComponentDescriptor.ExpressionType.ComponentReference;
				}
			}

			// Token: 0x06001000 RID: 4096 RVA: 0x0003772F File Offset: 0x0003592F
			public override void AppendValue(JavaScriptSerializer serializer, StringBuilder builder)
			{
				builder.Append('"');
				builder.Append(HttpUtility.JavaScriptStringEncode(this._componentID));
				builder.Append('"');
			}

			// Token: 0x040004D9 RID: 1241
			private string _componentID;
		}

		// Token: 0x02000160 RID: 352
		private sealed class ElementReference : ScriptComponentDescriptor.Expression
		{
			// Token: 0x06001001 RID: 4097 RVA: 0x00037755 File Offset: 0x00035955
			public ElementReference(string elementID)
			{
				this._elementID = elementID;
			}

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x06001002 RID: 4098 RVA: 0x0001359B File Offset: 0x0001179B
			public override ScriptComponentDescriptor.ExpressionType Type
			{
				get
				{
					return ScriptComponentDescriptor.ExpressionType.Script;
				}
			}

			// Token: 0x06001003 RID: 4099 RVA: 0x00037764 File Offset: 0x00035964
			public override void AppendValue(JavaScriptSerializer serializer, StringBuilder builder)
			{
				builder.Append("$get(\"");
				builder.Append(HttpUtility.JavaScriptStringEncode(this._elementID));
				builder.Append("\")");
			}

			// Token: 0x040004DA RID: 1242
			private string _elementID;
		}

		// Token: 0x02000161 RID: 353
		private sealed class ObjectReference : ScriptComponentDescriptor.Expression
		{
			// Token: 0x06001004 RID: 4100 RVA: 0x00037790 File Offset: 0x00035990
			public ObjectReference(object value)
			{
				this._value = value;
			}

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x06001005 RID: 4101 RVA: 0x0001359B File Offset: 0x0001179B
			public override ScriptComponentDescriptor.ExpressionType Type
			{
				get
				{
					return ScriptComponentDescriptor.ExpressionType.Script;
				}
			}

			// Token: 0x06001006 RID: 4102 RVA: 0x0003779F File Offset: 0x0003599F
			public override void AppendValue(JavaScriptSerializer serializer, StringBuilder builder)
			{
				serializer.Serialize(this._value, builder, JavaScriptSerializer.SerializationFormat.JavaScript);
			}

			// Token: 0x040004DB RID: 1243
			private object _value;
		}

		// Token: 0x02000162 RID: 354
		private sealed class ScriptExpression : ScriptComponentDescriptor.Expression
		{
			// Token: 0x06001007 RID: 4103 RVA: 0x000377AF File Offset: 0x000359AF
			public ScriptExpression(string script)
			{
				this._script = script;
			}

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x06001008 RID: 4104 RVA: 0x0001359B File Offset: 0x0001179B
			public override ScriptComponentDescriptor.ExpressionType Type
			{
				get
				{
					return ScriptComponentDescriptor.ExpressionType.Script;
				}
			}

			// Token: 0x06001009 RID: 4105 RVA: 0x000377BE File Offset: 0x000359BE
			public override void AppendValue(JavaScriptSerializer serializer, StringBuilder builder)
			{
				builder.Append(this._script);
			}

			// Token: 0x040004DC RID: 1244
			private string _script;
		}
	}
}
