using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.Membership;
using TechnoPro.Common.DAO.Membership;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.DAO.Impl.IoC
{
	// Token: 0x020000B0 RID: 176
	public class CommonDAOImplConventionInjection : ConventionInjection
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0002B9FC File Offset: 0x00029BFC
		public CommonDAOImplConventionInjection()
		{
			this.DefaultObjectMap = new Dictionary<Type, IcwObject>
			{
				{
					typeof(IMediaContentDAO),
					this.RetrieveIcwObject<MediaContentDAO>(DefaultLifetime.Transient.ToString())
				},
				{
					typeof(IUserDAO),
					this.RetrieveIcwObject<UserDAO>(DefaultLifetime.Transient.ToString())
				}
			};
		}
	}
}
