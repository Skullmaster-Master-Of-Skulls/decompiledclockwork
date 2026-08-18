using System;
using System.Deployment.Internal.Isolation.Manifest;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000066 RID: 102
	internal class ApplicationContext
	{
		// Token: 0x060001EF RID: 495 RVA: 0x000086F1 File Offset: 0x000068F1
		internal ApplicationContext(IActContext a)
		{
			if (a == null)
			{
				throw new ArgumentNullException();
			}
			this._appcontext = a;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008709 File Offset: 0x00006909
		public ApplicationContext(DefinitionAppId appid)
		{
			if (appid == null)
			{
				throw new ArgumentNullException();
			}
			this._appcontext = IsolationInterop.CreateActContext(appid._id);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000872B File Offset: 0x0000692B
		public ApplicationContext(ReferenceAppId appid)
		{
			if (appid == null)
			{
				throw new ArgumentNullException();
			}
			this._appcontext = IsolationInterop.CreateActContext(appid._id);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008750 File Offset: 0x00006950
		public DefinitionAppId Identity
		{
			get
			{
				object obj;
				this._appcontext.GetAppId(out obj);
				return new DefinitionAppId(obj as IDefinitionAppId);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008778 File Offset: 0x00006978
		public string BasePath
		{
			get
			{
				string result;
				this._appcontext.ApplicationBasePath(0U, out result);
				return result;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008794 File Offset: 0x00006994
		public string ReplaceStrings(string culture, string toreplace)
		{
			string result;
			this._appcontext.ReplaceStringMacros(0U, culture, toreplace, out result);
			return result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000087B4 File Offset: 0x000069B4
		internal ICMS GetComponentManifest(DefinitionIdentity component)
		{
			object obj;
			this._appcontext.GetComponentManifest(0U, component._id, ref IsolationInterop.IID_ICMS, out obj);
			return obj as ICMS;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000087E0 File Offset: 0x000069E0
		internal string GetComponentManifestPath(DefinitionIdentity component)
		{
			object obj;
			this._appcontext.GetComponentManifest(0U, component._id, ref IsolationInterop.IID_IManifestInformation, out obj);
			string result;
			((IManifestInformation)obj).get_FullPath(out result);
			return result;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008814 File Offset: 0x00006A14
		public string GetComponentPath(DefinitionIdentity component)
		{
			string result;
			this._appcontext.GetComponentPayloadPath(0U, component._id, out result);
			return result;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008838 File Offset: 0x00006A38
		public DefinitionIdentity MatchReference(ReferenceIdentity TheRef)
		{
			object obj;
			this._appcontext.FindReferenceInContext(0U, TheRef._id, out obj);
			return new DefinitionIdentity(obj as IDefinitionIdentity);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00008864 File Offset: 0x00006A64
		public EnumDefinitionIdentity Components
		{
			get
			{
				object obj;
				this._appcontext.EnumComponents(0U, out obj);
				return new EnumDefinitionIdentity(obj as IEnumDefinitionIdentity);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000888A File Offset: 0x00006A8A
		public void PrepareForExecution()
		{
			this._appcontext.PrepareForExecution(IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000088A4 File Offset: 0x00006AA4
		public ApplicationContext.ApplicationStateDisposition SetApplicationState(ApplicationContext.ApplicationState s)
		{
			uint result;
			this._appcontext.SetApplicationRunningState(0U, (uint)s, out result);
			return (ApplicationContext.ApplicationStateDisposition)result;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000088C4 File Offset: 0x00006AC4
		public string StateLocation
		{
			get
			{
				string result;
				this._appcontext.GetApplicationStateFilesystemLocation(0U, UIntPtr.Zero, IntPtr.Zero, out result);
				return result;
			}
		}

		// Token: 0x040001BB RID: 443
		private IActContext _appcontext;

		// Token: 0x02000541 RID: 1345
		public enum ApplicationState
		{
			// Token: 0x04003803 RID: 14339
			Undefined,
			// Token: 0x04003804 RID: 14340
			Starting,
			// Token: 0x04003805 RID: 14341
			Running
		}

		// Token: 0x02000542 RID: 1346
		public enum ApplicationStateDisposition
		{
			// Token: 0x04003807 RID: 14343
			Undefined,
			// Token: 0x04003808 RID: 14344
			Starting,
			// Token: 0x04003809 RID: 14345
			Starting_Migrated = 65537,
			// Token: 0x0400380A RID: 14346
			Running = 2,
			// Token: 0x0400380B RID: 14347
			Running_FirstTime = 131074
		}
	}
}
