
using AFI.BusinessLogic.Entities;
using AFI.Models.Client;

namespace AFI.Models.Adapters
{
    public class PolicyHolderAdapter : BaseAdapter<PolicyHolder, Person>, IPolicyHolderAdapter
    {
        public override PolicyHolder ToModel(Person entity)
        {
            throw new NotImplementedException("Not going to implement this, there is not enough time; just wanted to show off some patterns");
        }

        public override Person ToEntity(PolicyHolder model, Person entity)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.DateOfBirth = model.DateOfBirth;

            if (!entity.Policies.Any(x => x.PolicyNumber == model.PolicyNumber))
            {
                var policy = new Policy
                {
                    PolicyNumber = model.PolicyNumber
                };

                entity.Policies.Add(policy);
            }

            if (string.IsNullOrEmpty(model.Email) ||
                entity.Contacts.Any(x => x.ContactType == ContactType.Email && x.Value == model.Email))
                return entity;

            var contact = new Contact()
            {
                Value = model.Email,
                ContactType = ContactType.Email
            };

            entity.Contacts.Add(contact);
            return entity;
        }
    }
}
