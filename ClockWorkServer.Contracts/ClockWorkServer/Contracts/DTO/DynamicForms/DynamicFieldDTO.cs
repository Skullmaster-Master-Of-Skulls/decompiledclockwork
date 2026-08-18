using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000673 RID: 1651
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicFieldDTO : ICloneable<DynamicFieldDTO>, ICloneable
	{
		// Token: 0x06002188 RID: 8584 RVA: 0x000036BD File Offset: 0x000018BD
		public DynamicFieldDTO()
		{
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0000F370 File Offset: 0x0000D570
		public DynamicFieldDTO(DynamicFieldDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.ControlId = item.ControlId;
				this.ControlCaption = item.ControlCaption;
				this.ControlCode = item.ControlCode;
				this.Args = ((item.Args == null) ? null : new Dictionary<string, string>(item.Args));
				this.IsActive = item.IsActive;
				this.OrderNum = item.OrderNum;
				this.ControlName = item.ControlName;
				this.IsReadOnly = item.IsReadOnly;
				this.HideCaption = item.HideCaption;
				this.DontWrapToNextLine = item.DontWrapToNextLine;
				this.Setting1 = item.Setting1;
				this.Setting2 = item.Setting2;
				this.Setting3 = item.Setting3;
				this.Setting4 = item.Setting4;
				this.DefaultValue = item.DefaultValue;
				this.DefaultValueString = item.DefaultValueString;
				this.Setting4String = item.Setting4String;
				this.Mask = item.Mask;
				this.EnforceMethod = item.EnforceMethod;
				this.OriginalCaption = item.OriginalCaption;
				this.UniqueId = item.UniqueId;
				this.SpecialControlType = item.SpecialControlType;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0000F4D1 File Offset: 0x0000D6D1
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x0000F4D9 File Offset: 0x0000D6D9
		[DataMember]
		public string ControlCaption { get; set; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0000F4E2 File Offset: 0x0000D6E2
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x0000F4EA File Offset: 0x0000D6EA
		[DataMember]
		public eControlCode ControlCode { get; set; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0000F4F3 File Offset: 0x0000D6F3
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x0000F4FB File Offset: 0x0000D6FB
		[DataMember]
		public Dictionary<string, string> Args { get; set; }

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0000F504 File Offset: 0x0000D704
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x0000F50C File Offset: 0x0000D70C
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0000F515 File Offset: 0x0000D715
		// (set) Token: 0x06002195 RID: 8597 RVA: 0x0000F51D File Offset: 0x0000D71D
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0000F526 File Offset: 0x0000D726
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x0000F52E File Offset: 0x0000D72E
		[DataMember]
		public string ControlName { get; set; }

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x0000F537 File Offset: 0x0000D737
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x0000F53F File Offset: 0x0000D73F
		[DataMember]
		public bool IsReadOnly { get; set; }

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x0000F548 File Offset: 0x0000D748
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x0000F550 File Offset: 0x0000D750
		[DataMember]
		public bool HideCaption { get; set; }

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0000F559 File Offset: 0x0000D759
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x0000F561 File Offset: 0x0000D761
		[DataMember]
		public bool DontWrapToNextLine { get; set; }

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x0000F56A File Offset: 0x0000D76A
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x0000F572 File Offset: 0x0000D772
		[DataMember]
		public int Setting1 { get; set; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x0000F57B File Offset: 0x0000D77B
		// (set) Token: 0x060021A1 RID: 8609 RVA: 0x0000F583 File Offset: 0x0000D783
		[DataMember]
		public int Setting2 { get; set; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0000F58C File Offset: 0x0000D78C
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x0000F594 File Offset: 0x0000D794
		[DataMember]
		public int Setting3 { get; set; }

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x0000F59D File Offset: 0x0000D79D
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x0000F5A5 File Offset: 0x0000D7A5
		[DataMember]
		public int Setting4 { get; set; }

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x0000F5AE File Offset: 0x0000D7AE
		// (set) Token: 0x060021A7 RID: 8615 RVA: 0x0000F5B6 File Offset: 0x0000D7B6
		[DataMember]
		public int DefaultValue { get; set; }

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0000F5BF File Offset: 0x0000D7BF
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x0000F5C7 File Offset: 0x0000D7C7
		[DataMember]
		public string DefaultValueString { get; set; }

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		[DataMember]
		public string Setting4String { get; set; }

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x0000F5E1 File Offset: 0x0000D7E1
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x0000F5E9 File Offset: 0x0000D7E9
		[DataMember]
		public string Mask { get; set; }

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x0000F5F2 File Offset: 0x0000D7F2
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x0000F5FA File Offset: 0x0000D7FA
		[DataMember]
		public eEnforceTypeDTO EnforceMethod { get; set; }

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0000F603 File Offset: 0x0000D803
		// (set) Token: 0x060021B1 RID: 8625 RVA: 0x0000F60B File Offset: 0x0000D80B
		[DataMember]
		public string OriginalCaption { get; set; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x0000F614 File Offset: 0x0000D814
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x0000F61C File Offset: 0x0000D81C
		[DataMember]
		public string UniqueId { get; set; }

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0000F625 File Offset: 0x0000D825
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0000F62D File Offset: 0x0000D82D
		[DataMember]
		public eSpecialControlType SpecialControlType { get; set; }

		// Token: 0x060021B6 RID: 8630 RVA: 0x0000F638 File Offset: 0x0000D838
		public DynamicFieldDTO Clone()
		{
			return new DynamicFieldDTO(this);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0000F650 File Offset: 0x0000D850
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
