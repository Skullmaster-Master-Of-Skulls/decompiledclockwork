using System;

namespace System.Web
{
	// Token: 0x020000BD RID: 189
	internal class HttpStaticObjectsEntry
	{
		// Token: 0x06000D30 RID: 3376 RVA: 0x00024F43 File Offset: 0x00023143
		internal HttpStaticObjectsEntry(string name, Type t, bool lateBound)
		{
			this._name = name;
			this._type = t;
			this._lateBound = lateBound;
			this._instance = null;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00024F67 File Offset: 0x00023167
		internal HttpStaticObjectsEntry(string name, object instance, int dummy)
		{
			this._name = name;
			this._type = instance.GetType();
			this._instance = instance;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00024F89 File Offset: 0x00023189
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00024F91 File Offset: 0x00023191
		internal Type ObjectType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00024F99 File Offset: 0x00023199
		internal bool LateBound
		{
			get
			{
				return this._lateBound;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00024FA1 File Offset: 0x000231A1
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

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00024FBC File Offset: 0x000231BC
		internal bool HasInstance
		{
			get
			{
				return this._instance != null;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00024FC8 File Offset: 0x000231C8
		internal object Instance
		{
			get
			{
				if (this._instance == null)
				{
					lock (this)
					{
						if (this._instance == null)
						{
							this._instance = Activator.CreateInstance(this._type);
						}
					}
				}
				return this._instance;
			}
		}

		// Token: 0x040004E4 RID: 1252
		private string _name;

		// Token: 0x040004E5 RID: 1253
		private Type _type;

		// Token: 0x040004E6 RID: 1254
		private bool _lateBound;

		// Token: 0x040004E7 RID: 1255
		private object _instance;
	}
}
