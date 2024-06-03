using AutoMapper;
using StudenProject.DataModels;
using StudenProject.DomainModels;

namespace StudenProject.Profiles.AfterMap
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, DataModels.Student>
    {
        public void Process(AddStudentRequest source, Student destination, ResolutionContext context)
        {
            // Random sınıfından bir örnek oluşturun
            Random random = new Random();

            // Rastgele bir int değeri üretin
            int newStudentId = random.Next();

            // destination.Id'yi bu yeni değere ayarlayın
            destination.Id = newStudentId;

            // destination.Address içindeki Id değerini değiştirin
            destination.Address = new DataModels.Address()
            {
                Id = newStudentId,
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };


        }
    }
}
