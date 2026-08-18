using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x0200009E RID: 158
	public class FieldGetter : MemberGetter
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00012BC0 File Offset: 0x00010DC0
		public FieldGetter(FieldInfo fieldInfo)
		{
			this._fieldInfo = fieldInfo;
			this.Name = fieldInfo.Name;
			this.MemberType = fieldInfo.FieldType;
			this._lateBoundFieldGet = new Lazy<LateBoundFieldGet>(() => MemberGetter.DelegateFactory.CreateGet(fieldInfo));
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00012C25 File Offset: 0x00010E25
		public override MemberInfo MemberInfo
		{
			get
			{
				return this._fieldInfo;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00012C2D File Offset: 0x00010E2D
		public override string Name { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00012C35 File Offset: 0x00010E35
		public override Type MemberType { get; }

		// Token: 0x0600049F RID: 1183 RVA: 0x00012C3D File Offset: 0x00010E3D
		public override object GetValue(object source)
		{
			return this._lateBoundFieldGet.Value(source);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00012C50 File Offset: 0x00010E50
		public bool Equals(FieldGetter other)
		{
			return other != null && (this == other || object.Equals(other._fieldInfo, this._fieldInfo));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00012C6E File Offset: 0x00010E6E
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(FieldGetter)) && this.Equals((FieldGetter)obj)));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00012CA0 File Offset: 0x00010EA0
		public override int GetHashCode()
		{
			return this._fieldInfo.GetHashCode();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00012CAD File Offset: 0x00010EAD
		public override IEnumerable<object> GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._fieldInfo.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00012CBC File Offset: 0x00010EBC
		public override IEnumerable<object> GetCustomAttributes(bool inherit)
		{
			return this._fieldInfo.GetCustomAttributes(inherit);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00012CCA File Offset: 0x00010ECA
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._fieldInfo.IsDefined(attributeType, inherit);
		}

		// Token: 0x040000DD RID: 221
		private readonly FieldInfo _fieldInfo;

		// Token: 0x040000DE RID: 222
		private readonly Lazy<LateBoundFieldGet> _lateBoundFieldGet;
	}
}
