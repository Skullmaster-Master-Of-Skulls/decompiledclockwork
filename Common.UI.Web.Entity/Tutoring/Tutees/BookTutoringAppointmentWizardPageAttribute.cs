using System;
using System.Linq;

namespace TechnoPro.Common.UI.Web.Entity.Tutoring.Tutees
{
	// Token: 0x02000026 RID: 38
	public class BookTutoringAppointmentWizardPageAttribute : Attribute
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00002050 File Offset: 0x00000250
		public BookTutoringAppointmentWizardPageAttribute()
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public BookTutoringAppointmentWizardPageAttribute(string title, string controlName, string imageUrl, string selectedImageUrl, string disabledImageUrl)
		{
			this.Title = title;
			this.ControlName = controlName;
			this.SelectedImageUrl = selectedImageUrl;
			this.DisabledImageUrl = disabledImageUrl;
			this.ImageUrl = imageUrl;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002C09 File Offset: 0x00000E09
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002C11 File Offset: 0x00000E11
		public string Title { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002C1A File Offset: 0x00000E1A
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002C22 File Offset: 0x00000E22
		public string ImageUrl { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002C2B File Offset: 0x00000E2B
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002C33 File Offset: 0x00000E33
		public string SelectedImageUrl { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002C3C File Offset: 0x00000E3C
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00002C44 File Offset: 0x00000E44
		public string DisabledImageUrl { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002C4D File Offset: 0x00000E4D
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00002C55 File Offset: 0x00000E55
		public string ControlName { get; set; }

		// Token: 0x060000FC RID: 252 RVA: 0x00002C60 File Offset: 0x00000E60
		public static BookTutoringAppointmentWizardPageAttribute GetAttribute(eBookTutoringAppointmentWizardPage clockWorkWebPageModule)
		{
			return BookTutoringAppointmentWizardPageAttribute.GetAttribute<BookTutoringAppointmentWizardPageAttribute>(clockWorkWebPageModule);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002C80 File Offset: 0x00000E80
		public static T GetAttribute<T>(Enum enumeration) where T : Attribute
		{
			T t = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault<T>();
			bool flag = t == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				result = t;
			}
			return result;
		}
	}
}
