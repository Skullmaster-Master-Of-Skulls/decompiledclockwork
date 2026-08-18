using System;
using System.Web.Mvc;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Binders.Adapters;

namespace TechnoPro.ClockWorkWeb.Binders
{
	// Token: 0x0200015D RID: 349
	public class PersonBaseModelBinder : DefaultModelBinder
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x00048B90 File Offset: 0x00046D90
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			bool flag = bindingContext.ModelType == typeof(PersonBaseDTO);
			object result;
			if (flag)
			{
				int num = Convert.ToInt32(bindingContext.GetValue("personid"));
				bool flag2 = num <= 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string value = bindingContext.GetValue("firstname");
					string value2 = bindingContext.GetValue("middlename");
					string value3 = bindingContext.GetValue("lastname");
					string value4 = bindingContext.GetValue("student_no");
					result = new PersonBaseDTO
					{
						PersonId = num,
						FirstName = value,
						MiddleName = value2,
						LastName = value3,
						Student_no = value4
					};
				}
			}
			else
			{
				result = base.BindModel(controllerContext, bindingContext);
			}
			return result;
		}
	}
}
