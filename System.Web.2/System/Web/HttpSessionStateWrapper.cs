using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web.SessionState;

namespace System.Web
{
	// Token: 0x02000035 RID: 53
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpSessionStateWrapper : HttpSessionStateBase
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00005C9B File Offset: 0x00003E9B
		public HttpSessionStateWrapper(HttpSessionState httpSessionState)
		{
			if (httpSessionState == null)
			{
				throw new ArgumentNullException("httpSessionState");
			}
			this._session = httpSessionState;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00005CB8 File Offset: 0x00003EB8
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00005CC5 File Offset: 0x00003EC5
		public override int CodePage
		{
			get
			{
				return this._session.CodePage;
			}
			set
			{
				this._session.CodePage = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00004335 File Offset: 0x00002535
		public override HttpSessionStateBase Contents
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00005CD3 File Offset: 0x00003ED3
		public override HttpCookieMode CookieMode
		{
			get
			{
				return this._session.CookieMode;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public override bool IsCookieless
		{
			get
			{
				return this._session.IsCookieless;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00005CED File Offset: 0x00003EED
		public override bool IsNewSession
		{
			get
			{
				return this._session.IsNewSession;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00005CFA File Offset: 0x00003EFA
		public override bool IsReadOnly
		{
			get
			{
				return this._session.IsReadOnly;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00005D07 File Offset: 0x00003F07
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this._session.Keys;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00005D21 File Offset: 0x00003F21
		public override int LCID
		{
			get
			{
				return this._session.LCID;
			}
			set
			{
				this._session.LCID = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00005D2F File Offset: 0x00003F2F
		public override SessionStateMode Mode
		{
			get
			{
				return this._session.Mode;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00005D3C File Offset: 0x00003F3C
		public override string SessionID
		{
			get
			{
				return this._session.SessionID;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00005D49 File Offset: 0x00003F49
		public override HttpStaticObjectsCollectionBase StaticObjects
		{
			get
			{
				return new HttpStaticObjectsCollectionWrapper(this._session.StaticObjects);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00005D5B File Offset: 0x00003F5B
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00005D68 File Offset: 0x00003F68
		public override int Timeout
		{
			get
			{
				return this._session.Timeout;
			}
			set
			{
				this._session.Timeout = value;
			}
		}

		// Token: 0x17000225 RID: 549
		public override object this[int index]
		{
			get
			{
				return this._session[index];
			}
			set
			{
				this._session[index] = value;
			}
		}

		// Token: 0x17000226 RID: 550
		public override object this[string name]
		{
			get
			{
				return this._session[name];
			}
			set
			{
				this._session[name] = value;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public override void Abandon()
		{
			this._session.Abandon();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00005DBD File Offset: 0x00003FBD
		public override void Add(string name, object value)
		{
			this._session.Add(name, value);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00005DCC File Offset: 0x00003FCC
		public override void Clear()
		{
			this._session.Clear();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00005DD9 File Offset: 0x00003FD9
		public override void Remove(string name)
		{
			this._session.Remove(name);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00005DE7 File Offset: 0x00003FE7
		public override void RemoveAll()
		{
			this._session.RemoveAll();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public override void RemoveAt(int index)
		{
			this._session.RemoveAt(index);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00005E02 File Offset: 0x00004002
		public override void CopyTo(Array array, int index)
		{
			this._session.CopyTo(array, index);
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00005E11 File Offset: 0x00004011
		public override int Count
		{
			get
			{
				return this._session.Count;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00005E1E File Offset: 0x0000401E
		public override bool IsSynchronized
		{
			get
			{
				return this._session.IsSynchronized;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00005E2B File Offset: 0x0000402B
		public override object SyncRoot
		{
			get
			{
				return this._session.SyncRoot;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00005E38 File Offset: 0x00004038
		public override IEnumerator GetEnumerator()
		{
			return this._session.GetEnumerator();
		}

		// Token: 0x04000110 RID: 272
		private readonly HttpSessionState _session;
	}
}
