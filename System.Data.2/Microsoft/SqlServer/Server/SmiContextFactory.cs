using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003B RID: 59
	internal sealed class SmiContextFactory
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x00039BC0 File Offset: 0x00038FC0
		private SmiContextFactory()
		{
			if (InOutOfProcHelper.InProc)
			{
				Type type = Type.GetType("Microsoft.SqlServer.Server.InProcLink, SqlAccess, PublicKeyToken=89845dcd8080cc91");
				if (null == type)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				FieldInfo staticField = this.GetStaticField(type, "Instance");
				if (!(staticField != null))
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00039D3C File Offset: 0x0003913C
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00039D60 File Offset: 0x00039160
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

		// Token: 0x060001CC RID: 460 RVA: 0x00039D84 File Offset: 0x00039184
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

		// Token: 0x060001CD RID: 461 RVA: 0x00039DCC File Offset: 0x000391CC
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private object GetValue(FieldInfo fieldInfo)
		{
			return fieldInfo.GetValue(null);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00039DE4 File Offset: 0x000391E4
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private FieldInfo GetStaticField(Type aType, string fieldName)
		{
			return aType.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
		}

		// Token: 0x040000F9 RID: 249
		public static readonly SmiContextFactory Instance = new SmiContextFactory();

		// Token: 0x040000FA RID: 250
		private readonly SmiLink _smiLink;

		// Token: 0x040000FB RID: 251
		private readonly ulong _negotiatedSmiVersion;

		// Token: 0x040000FC RID: 252
		private readonly byte _majorVersion;

		// Token: 0x040000FD RID: 253
		private readonly byte _minorVersion;

		// Token: 0x040000FE RID: 254
		private readonly short _buildNum;

		// Token: 0x040000FF RID: 255
		private readonly string _serverVersion;

		// Token: 0x04000100 RID: 256
		private readonly SmiEventSink_Default _eventSinkForGetCurrentContext;

		// Token: 0x04000101 RID: 257
		internal const ulong YukonVersion = 100UL;

		// Token: 0x04000102 RID: 258
		internal const ulong KatmaiVersion = 210UL;

		// Token: 0x04000103 RID: 259
		internal const ulong LatestVersion = 210UL;

		// Token: 0x04000104 RID: 260
		private readonly ulong[] __supportedSmiVersions = new ulong[]
		{
			100UL,
			210UL
		};
	}
}
