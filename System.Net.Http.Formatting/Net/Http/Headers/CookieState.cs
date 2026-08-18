using System;
using System.Collections.Specialized;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http.Headers
{
	// Token: 0x02000036 RID: 54
	public class CookieState : ICloneable
	{
		// Token: 0x0600019E RID: 414 RVA: 0x000077F5 File Offset: 0x000059F5
		public CookieState(string name) : this(name, string.Empty)
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007803 File Offset: 0x00005A03
		public CookieState(string name, string value)
		{
			this._values = HttpValueCollection.Create();
			base..ctor();
			CookieState.CheckNameFormat(name, "name");
			this._name = name;
			CookieState.CheckValueFormat(value, "value");
			this.Value = value;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000783A File Offset: 0x00005A3A
		public CookieState(string name, NameValueCollection values)
		{
			this._values = HttpValueCollection.Create();
			base..ctor();
			CookieState.CheckNameFormat(name, "name");
			this._name = name;
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			this.Values.Add(values);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007879 File Offset: 0x00005A79
		private CookieState(CookieState source)
		{
			this._values = HttpValueCollection.Create();
			base..ctor();
			this._name = source._name;
			if (source._values != null)
			{
				this.Values.Add(source._values);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000078B1 File Offset: 0x00005AB1
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000078B9 File Offset: 0x00005AB9
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				CookieState.CheckNameFormat(value, "value");
				this._name = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000078CD File Offset: 0x00005ACD
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000078F0 File Offset: 0x00005AF0
		public string Value
		{
			get
			{
				if (this.Values.Count <= 0)
				{
					return string.Empty;
				}
				return this.Values.AllKeys[0];
			}
			set
			{
				CookieState.CheckValueFormat(value, "value");
				if (this.Values.Count > 0)
				{
					this.Values.AllKeys[0] = value;
					return;
				}
				this.Values.Add(value, string.Empty);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000792B File Offset: 0x00005B2B
		public NameValueCollection Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x17000049 RID: 73
		public string this[string subName]
		{
			get
			{
				return this.Values[subName];
			}
			set
			{
				this.Values[subName] = value;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007950 File Offset: 0x00005B50
		public override string ToString()
		{
			return this._name + "=" + ((this._values != null) ? this._values.ToString() : string.Empty);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000797C File Offset: 0x00005B7C
		public object Clone()
		{
			return new CookieState(this);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007984 File Offset: 0x00005B84
		private static void CheckNameFormat(string name, string parameterName)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (!FormattingUtilities.ValidateHeaderToken(name))
			{
				throw Error.Argument(parameterName, Resources.CookieInvalidName, new object[0]);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000079AE File Offset: 0x00005BAE
		private static void CheckValueFormat(string value, string parameterName)
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
		}

		// Token: 0x04000082 RID: 130
		private string _name;

		// Token: 0x04000083 RID: 131
		private NameValueCollection _values;
	}
}
