using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000B0 RID: 176
	public class MethodGetter : MemberGetter
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public MethodGetter(MethodInfo methodInfo)
		{
			this._methodInfo = methodInfo;
			this.Name = this._methodInfo.Name;
			this._memberType = this._methodInfo.ReturnType;
			this._lateBoundMethod = new Lazy<LateBoundMethod>(() => MemberGetter.DelegateFactory.CreateGet(methodInfo));
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00013C55 File Offset: 0x00011E55
		public override MemberInfo MemberInfo
		{
			get
			{
				return this._methodInfo;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00013C5D File Offset: 0x00011E5D
		public override string Name { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00013C65 File Offset: 0x00011E65
		public override Type MemberType
		{
			get
			{
				return this._memberType;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013C6D File Offset: 0x00011E6D
		public override object GetValue(object source)
		{
			if (!(this._memberType == null))
			{
				return this._lateBoundMethod.Value(source, new object[0]);
			}
			return null;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013C96 File Offset: 0x00011E96
		public override IEnumerable<object> GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._methodInfo.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00013CA5 File Offset: 0x00011EA5
		public override IEnumerable<object> GetCustomAttributes(bool inherit)
		{
			return this._methodInfo.GetCustomAttributes(inherit);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013CB3 File Offset: 0x00011EB3
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._methodInfo.IsDefined(attributeType, inherit);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00013CC2 File Offset: 0x00011EC2
		public bool Equals(MethodGetter other)
		{
			return other != null && (this == other || object.Equals(other._methodInfo, this._methodInfo));
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00013CE0 File Offset: 0x00011EE0
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(MethodGetter)) && this.Equals((MethodGetter)obj)));
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00013D12 File Offset: 0x00011F12
		public override int GetHashCode()
		{
			return this._methodInfo.GetHashCode();
		}

		// Token: 0x040000E9 RID: 233
		private readonly MethodInfo _methodInfo;

		// Token: 0x040000EA RID: 234
		private readonly Type _memberType;

		// Token: 0x040000EB RID: 235
		private readonly Lazy<LateBoundMethod> _lateBoundMethod;
	}
}
