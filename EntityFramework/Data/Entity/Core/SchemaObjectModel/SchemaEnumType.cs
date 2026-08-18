using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200038C RID: 908
	internal class SchemaEnumType : SchemaType
	{
		// Token: 0x060020D8 RID: 8408 RVA: 0x0009A95A File Offset: 0x00098B5A
		public SchemaEnumType(Schema parentElement) : base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0009A991 File Offset: 0x00098B91
		public bool IsFlags
		{
			get
			{
				return this._isFlags;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0009A999 File Offset: 0x00098B99
		public SchemaType UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0009A9A1 File Offset: 0x00098BA1
		public IEnumerable<SchemaEnumMember> EnumMembers
		{
			get
			{
				return this._enumMembers;
			}
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0009A9AC File Offset: 0x00098BAC
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
						this.SkipElement(reader);
						return true;
					}
					if (base.CanHandleElement(reader, "TypeAnnotation"))
					{
						this.SkipElement(reader);
						return true;
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0009AA0C File Offset: 0x00098C0C
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

		// Token: 0x060020DE RID: 8414 RVA: 0x0009AA64 File Offset: 0x00098C64
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

		// Token: 0x060020DF RID: 8415 RVA: 0x0009AB34 File Offset: 0x00098D34
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal override void ResolveTopLevelNames()
		{
			if (this._unresolvedUnderlyingTypeName == null)
			{
				this._underlyingType = base.Schema.SchemaManager.SchemaTypes.Single((SchemaType t) => t is ScalarType && ((ScalarType)t).TypeKind == PrimitiveTypeKind.Int32);
				return;
			}
			base.Schema.ResolveTypeName(this, this._unresolvedUnderlyingTypeName, out this._underlyingType);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x0009ABE8 File Offset: 0x00098DE8
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

		// Token: 0x04000B9E RID: 2974
		private bool _isFlags;

		// Token: 0x04000B9F RID: 2975
		private string _unresolvedUnderlyingTypeName;

		// Token: 0x04000BA0 RID: 2976
		private SchemaType _underlyingType;

		// Token: 0x04000BA1 RID: 2977
		private readonly IList<SchemaEnumMember> _enumMembers = new List<SchemaEnumMember>();
	}
}
