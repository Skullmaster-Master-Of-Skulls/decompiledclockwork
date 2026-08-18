using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoMapper
{
	// Token: 0x0200004E RID: 78
	public class MemberConfiguration : IMemberConfiguration
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00007B80 File Offset: 0x00005D80
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00007B88 File Offset: 0x00005D88
		public IParentSourceToDestinationNameMapper NameMapper { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00007B91 File Offset: 0x00005D91
		public IList<IChildMemberConfiguration> MemberMappers { get; } = new Collection<IChildMemberConfiguration>();

		// Token: 0x0600031A RID: 794 RVA: 0x00007B99 File Offset: 0x00005D99
		public IMemberConfiguration AddMember<TMemberMapper>(Action<TMemberMapper> setupAction = null) where TMemberMapper : IChildMemberConfiguration, new()
		{
			this.GetOrAdd<TMemberMapper>((IMemberConfiguration _) => (IList)_.MemberMappers, setupAction);
			return this;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00007BC3 File Offset: 0x00005DC3
		public IMemberConfiguration AddName<TNameMapper>(Action<TNameMapper> setupAction = null) where TNameMapper : ISourceToDestinationNameMapper, new()
		{
			this.GetOrAdd<TNameMapper>((IMemberConfiguration _) => (IList)_.NameMapper.NamedMappers, setupAction);
			return this;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00007BF0 File Offset: 0x00005DF0
		private TMemberMapper GetOrAdd<TMemberMapper>(Func<IMemberConfiguration, IList> getList, Action<TMemberMapper> setupAction = null) where TMemberMapper : new()
		{
			TMemberMapper tmemberMapper = getList(this).OfType<TMemberMapper>().FirstOrDefault<TMemberMapper>();
			if (tmemberMapper == null)
			{
				tmemberMapper = Activator.CreateInstance<TMemberMapper>();
				getList(this).Add(tmemberMapper);
			}
			if (setupAction != null)
			{
				setupAction(tmemberMapper);
			}
			return tmemberMapper;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00007C3B File Offset: 0x00005E3B
		public MemberConfiguration()
		{
			this.NameMapper = new ParentSourceToDestinationNameMapper();
			this.MemberMappers.Add(new DefaultMember
			{
				NameMapper = this.NameMapper
			});
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00007C78 File Offset: 0x00005E78
		public bool MapDestinationPropertyToSource(IProfileConfiguration options, TypeDetails sourceType, Type destType, string nameToSearch, LinkedList<IValueResolver> resolvers)
		{
			bool flag = false;
			foreach (IChildMemberConfiguration childMemberConfiguration in this.MemberMappers)
			{
				flag = childMemberConfiguration.MapDestinationPropertyToSource(options, sourceType, destType, nameToSearch, resolvers, this);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}
	}
}
