using System;
using System.Collections.Generic;

namespace TechnoPro.Common.UI.Web.Veterans.Controls
{
	// Token: 0x02000003 RID: 3
	public class VetTaskStep
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000032D8 File Offset: 0x000014D8
		public static VetTaskStep AddVetTaskStep(ref List<VetTaskStep> steps, int stepNum, eVetTaskStepType stepType)
		{
			VetTaskStepAttribute[] array = stepType.GetType().GetField(stepType.ToString()).GetCustomAttributes(typeof(VetTaskStepAttribute), false) as VetTaskStepAttribute[];
			VetTaskStep vetTaskStep;
			if (array.Length != 0)
			{
				vetTaskStep = new VetTaskStep
				{
					StepNum = stepNum,
					Id = string.Format("step{0}", stepNum.ToString()),
					Url = array[0].Url,
					Title = string.Format("{0}. {1}", stepNum.ToString(), array[0].Title),
					Description = array[0].Description,
					StepType = stepType
				};
			}
			else
			{
				vetTaskStep = new VetTaskStep
				{
					StepNum = stepNum,
					Id = string.Format("step{0}", stepNum.ToString()),
					Url = "",
					Title = stepNum.ToString(),
					Description = "",
					StepType = stepType
				};
			}
			steps.Add(vetTaskStep);
			return vetTaskStep;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000033DB File Offset: 0x000015DB
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000033E3 File Offset: 0x000015E3
		public eVetTaskStepType StepType { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000033EC File Offset: 0x000015EC
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000033F4 File Offset: 0x000015F4
		public int StepNum { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000033FD File Offset: 0x000015FD
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00003405 File Offset: 0x00001605
		public string Id { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000340E File Offset: 0x0000160E
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00003416 File Offset: 0x00001616
		public string Title { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000341F File Offset: 0x0000161F
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00003427 File Offset: 0x00001627
		public string Url { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003430 File Offset: 0x00001630
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00003438 File Offset: 0x00001638
		public string Description { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003441 File Offset: 0x00001641
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00003449 File Offset: 0x00001649
		public bool IsChecked { get; set; }
	}
}
