using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Conferences.Domain.Persistence
{
    public class SkipMap : Attribute {}
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {


        public override bool ShouldMap(System.Type type)
        {
            return (type.BaseType == typeof(Entity));
        }

        public override bool ShouldMap(Member member)
        {


            var explicitSkip = member.MemberInfo.GetCustomAttributes(typeof(SkipMap), false).Length > 0;
            if ((member.IsProperty && !member.CanWrite) || explicitSkip) return false;


            return base.ShouldMap(member);
        }


    }



    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "id");
        }
    }

    public class ManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            //Hack:  the cast is nessesary because the compiler tries to take the Method and not the property
            if ((IManyToManyCollectionInspector)instance.OtherSide != null)
            {
                if (((IManyToManyCollectionInspector)instance.OtherSide).Inverse)
                    instance.Table(string.Format("{0}To{1}", instance.EntityType.Name, instance.ChildType.Name));

            }
            else
                instance.Inverse();
            instance.Cascade.All();

        }
    }


    public class DefaultOneToManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(instance.EntityType.Name + "Id");
            instance.Cascade.All();

        }
    }
    public class DefaultReferencesConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(instance.Property.Name + "Id");
            instance.NotFound.Ignore();
            instance.Cascade.SaveUpdate();
        }
    }


    public class EnumConvention : IUserTypeConvention
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType.IsEnum);
        }

        public void Apply(IPropertyInstance target)
        {
            target.CustomType(target.Property.PropertyType);
        }
    }


  
}
