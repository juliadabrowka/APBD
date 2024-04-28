using cw3.Model;

namespace cw3.Repositories;

public interface IAnimalsRepository
{
    IEnumerable<Animal> GetAnimalsList();
    int CreateAnimal(Animal animal);
    Animal GetAnimal(int animalId);
    int UpdateAnimalInfo(Animal animal);
    int DeleteAnimalById(int animalId);
    
}