using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200030F RID: 783
	internal class SchemaEnumType : SchemaType
	{
		// Token: 0x06002E7F RID: 11903 RVA: 0x000AFB1A File Offset: 0x000ADD1A
		public SchemaEnumType(Schema parentElement) : base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000AFB51 File Offset: 0x000ADD51
		public bool IsFlags
		{
			get
			{
				return this._isFlags;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000AFB59 File Offset: 0x000ADD59
		public SchemaType UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000AFB61 File Offset: 0x000ADD61
		public IEnumerable<SchemaEnumMember> EnumMembers
		{
			get
			{
				return this._enumMembers;
			}
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000AFB6C File Offset: 0x000ADD6C
		protected override bool HandleElement(XmlReader reader)
		{
			if (!base.HandleElement(reader))
			{
				if (base.CanHandleElement(reader, "Member"))
				{
					this.HandleMemberElement(reader);
				}
				else
				{
					if (base.CanHandleElement(reader, "ValueAnnotation"))
					{
						base.SkipElement(reader);
						return true;
					}
					if (base.CanHandleElement(reader, "TypeAnnotation"))
					{
						base.SkipElement(reader);
						return true;
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000AFBCC File Offset: 0x000ADDCC
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (!base.HandleAttribute(reader))
			{
				if (SchemaElement.CanHandleAttribute(reader, "IsFlags"))
				{
					base.HandleBoolAttribute(reader, ref this._isFlags);
				}
				else
				{
					if (!SchemaElement.CanHandleAttribute(reader, "UnderlyingType"))
					{
						return false;
					}
					Utils.GetDottedName(base.Schema, reader, out this._unresolvedUnderlyingTypeName);
				}
			}
			return true;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000AFC24 File Offset: 0x000ADE24
		private void HandleMemberElement(XmlReader reader)
		{
			SchemaEnumMember schemaEnumMember = new SchemaEnumMember(this);
			schemaEnumMember.Parse(reader);
			if (schemaEnumMember.Value == null)
			{
				if (this._enumMembers.Count == 0)
				{
					schemaEnumMember.Value = new long?(0L);
				}
				else
				{
					long value = this._enumMembers[this._enumMembers.Count - 1].Value.Value;
					if (value < 9223372036854775807L)
					{
						schemaEnumMember.Value = new long?(value + 1L);
					}
					else
					{
						base.AddError(ErrorCode.CalculatedEnumValueOutOfRange, EdmSchemaErrorSeverity.Error, Strings.CalculatedEnumValueOutOfRange);
						schemaEnumMember.Value = new long?(value);
					}
				}
			}
			this._enumMembers.Add(schemaEnumMember);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000AFCD8 File Offset: 0x000ADED8
		internal override void ResolveTopLevelNames()
		{
			if (this._unresolvedUnderlyingTypeName == null)
			{
				this._underlyingType = base.Schema.SchemaManager.SchemaTypes.Single((SchemaType t) => t is ScalarType && ((ScalarType)t).TypeKind == PrimitiveTypeKind.Int32);
				return;
			}
			base.Schema.ResolveTypeName(this, this._unresolvedUnderlyingTypeName, out this._underlyingType);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000AFD44 File Offset: 0x000ADF44
		internal override void Validate()
		{
			base.Validate();
			ScalarType enumUnderlyingType = this.UnderlyingType as ScalarType;
			if (enumUnderlyingType == null || !Helper.IsSupportedEnumUnderlyingType(enumUnderlyingType.TypeKind))
			{
				base.AddError(ErrorCode.InvalidEnumUnderlyingType, EdmSchemaErrorSeverity.Error, Strings.InvalidEnumUnderlyingType);
			}
			else
			{
				IEnumerable<SchemaEnumMember> enumerable = from m in this._enumMembers
				where !Helper.IsEnumMemberValueInRange(enumUnderlyingType.TypeKind, m.Value.Value)
				select m;
				foreach (SchemaEnumMember schemaEnumMember in enumerable)
				{
					schemaEnumMember.AddError(ErrorCode.EnumMemberValueOutOfItsUnderylingTypeRange, EdmSchemaErrorSeverity.Error, Strings.EnumMemberValueOutOfItsUnderylingTypeRange(schemaEnumMember.Value, schemaEnumMember.Name, this.UnderlyingType.Name));
				}
			}
			if ((from o in this._enumMembers
			group o by o.Name into g
			where g.Count<SchemaEnumMember>() > 1
			select g).Any<IGrouping<string, SchemaEnumMember>>())
			{
				base.AddError(ErrorCode.DuplicateEnumMember, EdmSchemaErrorSeverity.Error, Strings.DuplicateEnumMember);
			}
		}

		// Token: 0x04001427 RID: 5159
		private bool _isFlags;

		// Token: 0x04001428 RID: 5160
		private string _unresolvedUnderlyingTypeName;

		// Token: 0x04001429 RID: 5161
		private SchemaType _underlyingType;

		// Token: 0x0400142A RID: 5162
		private readonly IList<SchemaEnumMember> _enumMembers = new List<SchemaEnumMember>();
	}
}
