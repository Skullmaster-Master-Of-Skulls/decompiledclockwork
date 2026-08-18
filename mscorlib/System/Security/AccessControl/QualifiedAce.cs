using System;
using System.Globalization;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000900 RID: 2304
	public abstract class QualifiedAce : KnownAce
	{
		// Token: 0x06005359 RID: 21337 RVA: 0x0012D754 File Offset: 0x0012C754
		private AceQualifier QualifierFromType(AceType type, out bool isCallback)
		{
			switch (type)
			{
			case AceType.AccessAllowed:
				isCallback = false;
				return AceQualifier.AccessAllowed;
			case AceType.AccessDenied:
				isCallback = false;
				return AceQualifier.AccessDenied;
			case AceType.SystemAudit:
				isCallback = false;
				return AceQualifier.SystemAudit;
			case AceType.SystemAlarm:
				isCallback = false;
				return AceQualifier.SystemAlarm;
			case AceType.AccessAllowedObject:
				isCallback = false;
				return AceQualifier.AccessAllowed;
			case AceType.AccessDeniedObject:
				isCallback = false;
				return AceQualifier.AccessDenied;
			case AceType.SystemAuditObject:
				isCallback = false;
				return AceQualifier.SystemAudit;
			case AceType.SystemAlarmObject:
				isCallback = false;
				return AceQualifier.SystemAlarm;
			case AceType.AccessAllowedCallback:
				isCallback = true;
				return AceQualifier.AccessAllowed;
			case AceType.AccessDeniedCallback:
				isCallback = true;
				return AceQualifier.AccessDenied;
			case AceType.AccessAllowedCallbackObject:
				isCallback = true;
				return AceQualifier.AccessAllowed;
			case AceType.AccessDeniedCallbackObject:
				isCallback = true;
				return AceQualifier.AccessDenied;
			case AceType.SystemAuditCallback:
				isCallback = true;
				return AceQualifier.SystemAudit;
			case AceType.SystemAlarmCallback:
				isCallback = true;
				return AceQualifier.SystemAlarm;
			case AceType.SystemAuditCallbackObject:
				isCallback = true;
				return AceQualifier.SystemAudit;
			case AceType.SystemAlarmCallbackObject:
				isCallback = true;
				return AceQualifier.SystemAlarm;
			}
			throw new SystemException();
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0012D804 File Offset: 0x0012C804
		internal QualifiedAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier sid, byte[] opaque) : base(type, flags, accessMask, sid)
		{
			this._qualifier = this.QualifierFromType(type, out this._isCallback);
			this.SetOpaque(opaque);
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x0600535B RID: 21339 RVA: 0x0012D82C File Offset: 0x0012C82C
		public AceQualifier AceQualifier
		{
			get
			{
				return this._qualifier;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x0600535C RID: 21340 RVA: 0x0012D834 File Offset: 0x0012C834
		public bool IsCallback
		{
			get
			{
				return this._isCallback;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x0600535D RID: 21341
		internal abstract int MaxOpaqueLengthInternal { get; }

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x0600535E RID: 21342 RVA: 0x0012D83C File Offset: 0x0012C83C
		public int OpaqueLength
		{
			get
			{
				if (this._opaque != null)
				{
					return this._opaque.Length;
				}
				return 0;
			}
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0012D850 File Offset: 0x0012C850
		public byte[] GetOpaque()
		{
			return this._opaque;
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0012D858 File Offset: 0x0012C858
		public void SetOpaque(byte[] opaque)
		{
			if (opaque != null)
			{
				if (opaque.Length > this.MaxOpaqueLengthInternal)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), new object[]
					{
						0,
						this.MaxOpaqueLengthInternal
					}));
				}
				if (opaque.Length % 4 != 0)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), new object[]
					{
						4
					}));
				}
			}
			this._opaque = opaque;
		}

		// Token: 0x04002B43 RID: 11075
		private readonly bool _isCallback;

		// Token: 0x04002B44 RID: 11076
		private readonly AceQualifier _qualifier;

		// Token: 0x04002B45 RID: 11077
		private byte[] _opaque;
	}
}
