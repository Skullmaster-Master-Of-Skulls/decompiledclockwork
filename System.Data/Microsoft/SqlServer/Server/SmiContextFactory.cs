using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000034 RID: 52
	internal sealed class SmiContextFactory
	{
		// Token: 0x060001CD RID: 461 RVA: 0x001DC998 File Offset: 0x001DBD98
		private SmiContextFactory()
		{
			if (InOutOfProcHelper.InProc)
			{
				Type type = Type.GetType("Microsoft.SqlServer.Server.InProcLink, SqlAccess, PublicKeyToken=89845dcd8080cc91");
				if (type == null)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				FieldInfo staticField = this.GetStaticField(type, "Instance");
				if (staticField == null)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				this._smiLink = (SmiLink)this.GetValue(staticField);
				FieldInfo staticField2 = this.GetStaticField(type, "BuildVersion");
				if (staticField2 != null)
				{
					uint num = (uint)this.GetValue(staticField2);
					this._majorVersion = (byte)(num >> 24);
					this._minorVersion = (byte)(num >> 16 & 255U);
					this._buildNum = (short)(num & 65535U);
					this._serverVersion = string.Format(null, "{0:00}.{1:00}.{2:0000}", new object[]
					{
						this._majorVersion,
						(short)this._minorVersion,
						this._buildNum
					});
				}
				else
				{
					this._serverVersion = string.Empty;
				}
				this._negotiatedSmiVersion = this._smiLink.NegotiateVersion(210UL);
				bool flag = false;
				int num2 = 0;
				while (!flag && num2 < this.__supportedSmiVersions.Length)
				{
					if (this.__supportedSmiVersions[num2] == this._negotiatedSmiVersion)
					{
						flag = true;
					}
					num2++;
				}
				if (!flag)
				{
					this._smiLink = null;
				}
				this._eventSinkForGetCurrentContext = new SmiEventSink_Default();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001CE RID: 462 RVA: 0x001DCB18 File Offset: 0x001DBF18
		internal ulong NegotiatedSmiVersion
		{
			get
			{
				if (this._smiLink == null)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				return this._negotiatedSmiVersion;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001CF RID: 463 RVA: 0x001DCB48 File Offset: 0x001DBF48
		internal string ServerVersion
		{
			get
			{
				if (this._smiLink == null)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				return this._serverVersion;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x001DCB78 File Offset: 0x001DBF78
		internal SmiContext GetCurrentContext()
		{
			if (this._smiLink == null)
			{
				throw SQL.ContextUnavailableOutOfProc();
			}
			object currentContext = this._smiLink.GetCurrentContext(this._eventSinkForGetCurrentContext);
			this._eventSinkForGetCurrentContext.ProcessMessagesAndThrow();
			if (currentContext == null)
			{
				throw SQL.ContextUnavailableWhileInProc();
			}
			return (SmiContext)currentContext;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x001DCBC8 File Offset: 0x001DBFC8
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private object GetValue(FieldInfo fieldInfo)
		{
			return fieldInfo.GetValue(null);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x001DCBE8 File Offset: 0x001DBFE8
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private FieldInfo GetStaticField(Type aType, string fieldName)
		{
			return aType.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
		}

		// Token: 0x04000576 RID: 1398
		internal const ulong YukonVersion = 100UL;

		// Token: 0x04000577 RID: 1399
		internal const ulong KatmaiVersion = 210UL;

		// Token: 0x04000578 RID: 1400
		internal const ulong LatestVersion = 210UL;

		// Token: 0x04000579 RID: 1401
		public static readonly SmiContextFactory Instance = new SmiContextFactory();

		// Token: 0x0400057A RID: 1402
		private readonly SmiLink _smiLink;

		// Token: 0x0400057B RID: 1403
		private readonly ulong _negotiatedSmiVersion;

		// Token: 0x0400057C RID: 1404
		private readonly byte _majorVersion;

		// Token: 0x0400057D RID: 1405
		private readonly byte _minorVersion;

		// Token: 0x0400057E RID: 1406
		private readonly short _buildNum;

		// Token: 0x0400057F RID: 1407
		private readonly string _serverVersion;

		// Token: 0x04000580 RID: 1408
		private readonly SmiEventSink_Default _eventSinkForGetCurrentContext;

		// Token: 0x04000581 RID: 1409
		private readonly ulong[] __supportedSmiVersions = new ulong[]
		{
			100UL,
			210UL
		};
	}
}
