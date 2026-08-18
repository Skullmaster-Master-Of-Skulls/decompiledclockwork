using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002CD RID: 717
	public sealed class ObjectTagBuilder : ControlBuilder
	{
		// Token: 0x06002056 RID: 8278 RVA: 0x00067D68 File Offset: 0x00065F68
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
		{
			if (id == null)
			{
				throw new HttpException(SR.GetString("Object_tag_must_have_id"));
			}
			base.ID = id;
			string text = (string)attribs["scope"];
			if (text == null)
			{
				this._scope = ObjectTagScope.Default;
			}
			else if (StringUtil.EqualsIgnoreCase(text, "page"))
			{
				this._scope = ObjectTagScope.Page;
			}
			else if (StringUtil.EqualsIgnoreCase(text, "session"))
			{
				this._scope = ObjectTagScope.Session;
			}
			else if (StringUtil.EqualsIgnoreCase(text, "application"))
			{
				this._scope = ObjectTagScope.Application;
			}
			else
			{
				if (!StringUtil.EqualsIgnoreCase(text, "appinstance"))
				{
					throw new HttpException(SR.GetString("Invalid_scope", new object[]
					{
						text
					}));
				}
				this._scope = ObjectTagScope.AppInstance;
			}
			Util.GetAndRemoveBooleanAttribute(attribs, "latebinding", ref this._fLateBinding);
			string text2 = (string)attribs["class"];
			if (text2 != null)
			{
				this._type = parser.GetType(text2);
			}
			if (this._type == null)
			{
				text2 = (string)attribs["classid"];
				if (text2 != null)
				{
					Guid clsid = new Guid(text2);
					this._type = Type.GetTypeFromCLSID(clsid);
					if (this._type == null)
					{
						throw new HttpException(SR.GetString("Invalid_clsid", new object[]
						{
							text2
						}));
					}
					if (this._fLateBinding || Util.IsLateBoundComClassicType(this._type))
					{
						this._lateBound = true;
						this._clsid = text2;
					}
					else
					{
						parser.AddTypeDependency(this._type);
					}
				}
			}
			if (this._type == null)
			{
				text2 = (string)attribs["progid"];
				if (text2 != null)
				{
					this._type = Type.GetTypeFromProgID(text2);
					if (this._type == null)
					{
						throw new HttpException(SR.GetString("Invalid_progid", new object[]
						{
							text2
						}));
					}
					if (this._fLateBinding || Util.IsLateBoundComClassicType(this._type))
					{
						this._lateBound = true;
						this._progid = text2;
					}
					else
					{
						parser.AddTypeDependency(this._type);
					}
				}
			}
			if (this._type == null)
			{
				throw new HttpException(SR.GetString("Object_tag_must_have_class_classid_or_progid"));
			}
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00006164 File Offset: 0x00004364
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00006164 File Offset: 0x00004364
		public override void AppendLiteralString(string s)
		{
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00067F8F File Offset: 0x0006618F
		internal ObjectTagScope Scope
		{
			get
			{
				return this._scope;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x00067F97 File Offset: 0x00066197
		internal Type ObjectType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00067F9F File Offset: 0x0006619F
		internal bool LateBound
		{
			get
			{
				return this._lateBound;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00067FA7 File Offset: 0x000661A7
		internal Type DeclaredType
		{
			get
			{
				if (!this._lateBound)
				{
					return this.ObjectType;
				}
				return typeof(object);
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00067FC2 File Offset: 0x000661C2
		internal string Progid
		{
			get
			{
				return this._progid;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00067FCA File Offset: 0x000661CA
		internal string Clsid
		{
			get
			{
				return this._clsid;
			}
		}

		// Token: 0x04001B0E RID: 6926
		private ObjectTagScope _scope;

		// Token: 0x04001B0F RID: 6927
		private Type _type;

		// Token: 0x04001B10 RID: 6928
		private bool _lateBound;

		// Token: 0x04001B11 RID: 6929
		private string _progid;

		// Token: 0x04001B12 RID: 6930
		private string _clsid;

		// Token: 0x04001B13 RID: 6931
		private bool _fLateBinding;
	}
}
