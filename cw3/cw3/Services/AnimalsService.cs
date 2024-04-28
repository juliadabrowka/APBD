using cw3.Model;
using cw3.Repositories;

namespace cw3.Services;

public class AnimalsService : IAnimalsService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    public IEnumerable<Animal> GetAnimalsList()
    {
        return _animalsRepository.GetAnimalsList();
    }

    public int CreateAnimal(Animal animal)
    {
        return _animalsRepository.CreateAnimal(animal);
    }

    public Animal GetAnimal(int id)
    {
        return _animalsRepository.GetAnimal(id);
    }

    public int UpdateAnimal(Animal animal)
    {
        return _animalsRepository.UpdateAnimalInfo(animal);
    }

    public int DeleteAnimal(int id)
    {
        return _animalsRepository.DeleteAnimalById(id);
    }
}