using AutoMapper;
using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Domain.Enums;
using HS4_BlogProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task Create(CreateAuthorDTO model)
        {
            Author author  = _mapper.Map<Author>(model);

            await _authorRepository.Create(author);
        }

        public async Task Delete(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == id);
            author.DeleteDate = DateTime.Now;
            author.Status = Status.Passive;
            await _authorRepository.Delete(author);
        }

        public async Task<List<AuthorVM>> GetAuthors()
        {
            return await _authorRepository.GetFilteredList(
                 select: x => new AuthorVM
                 {
                     Id = x.Id,
                     FirstName=x.FirstName,
                     LastName=x.LastName,
                     ImagePath=x.ImagePath
                 },
                 where: x => x.Status != Status.Passive,
                 orderBy: x => x.OrderBy(x => x.FirstName)
                 );
        }

        public async Task<UpdateAuthorDTO> GetById(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateAuthorDTO>(author);
            return model;
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);
            await _authorRepository.Update(author);
        }
    }
}
