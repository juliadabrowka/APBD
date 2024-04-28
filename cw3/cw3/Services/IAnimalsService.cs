using cw3.Model;

namespace cw3.Services;

public interface IAnimalsService
{
    IEnumerable<Animal> GetAnimalsList();
    int CreateAnimal(Animal animal);
    Animal GetAnimal(int id);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int id);
}