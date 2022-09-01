using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<List<AuthorVM>> GetAuthors();
        Task Create(CreateAuthorDTO model);
        Task Update(UpdateAuthorDTO model);
        Task Delete(int id);
        Task<UpdateAuthorDTO> GetById(int id);

    }
}
