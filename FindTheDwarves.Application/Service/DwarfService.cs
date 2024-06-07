using FindTheDwarves.Application.DTO.Comments;
using FindTheDwarves.Application.DTO.Dwarves;
using FindTheDwarves.Application.Interface;
using FindTheDwarves.Domain.Interface;
using FindTheDwarves.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.Service
{
    public class DwarfService : IDwarfService
    {
        private readonly IDwarfRepository _dwarfRepository;

        public DwarfService(IDwarfRepository dwarfRepository)
        {
            _dwarfRepository = dwarfRepository;
        }

        public int AddComment(NewCommentDTO dto, int userID)
        {
            var dwarf = _dwarfRepository.GetDwarfByID(dto.DwarfID);
            if (dwarf == null)
            {
                return -1;
            }

            if (_dwarfRepository.CanBeCommented(userID, dto.DwarfID) == false)
            {
                return -2;
            }

            Comment comment= new Comment();

            comment.Text = dto.Text;
            comment.UserID = userID;
            comment.DwarfID = dto.DwarfID;

            var result = _dwarfRepository.AddComment(comment);

            return result;
        }

        public int AddNewDwarf(NewDwarfDTO dto)
        {
            if (_dwarfRepository.GetDwarfByName(dto.Name)  != null)
            {
                return -1;
            }

            if (_dwarfRepository.GetDwarfByCode(dto.ActivationCode) != null)
            {
                return -2;
            }

            Dwarf dwarf = new Dwarf()
            {
                Name = dto.Name,
                ActivationCode = dto.ActivationCode,
                Description = dto.Description
            };

            return _dwarfRepository.AddDwarf(dwarf);
        }

        public DwarfDetailsDTO ClaimDwarf(ClaimDwarfDTO dto, int userID)
        {


            var dwarf = _dwarfRepository.GetDwarfByCode(dto.ActivationCode);

            if (dwarf == null)
            {
                return null;
            }

            var userDwarf = new UserDwarf()
            {
                UserID = userID,
                DwarfID = dwarf.DwarfID
            };

            _dwarfRepository.ClaimDwarf(userDwarf);
            var result = GetDwarfByName(dwarf.Name, userID);
            return result;
        }

        public void DeleteDwarf(int dwarfID)
        {
            var dwarf = _dwarfRepository.GetDwarfByID(dwarfID);
            if (dwarf == null)
            {
                return;
            }

            _dwarfRepository.DeleteDwarf(dwarf);

            return;

        }

        public DwarfDetailsDTO GetDwarfByName(string name, int userID)
        {
            var dwarf = _dwarfRepository.GetDwarfByName(name);

            if (dwarf == null) 
            {
                return null;
            }

            if (!_dwarfRepository.CanBeCommented(userID,dwarf.DwarfID))
            {
                return null;
            }

            DwarfDetailsDTO dto = new DwarfDetailsDTO();

            dto.DwarfID = dwarf.DwarfID;
            dto.Name = dwarf.Name;
            dto.Description = dwarf.Description;

            var comments = _dwarfRepository.GetDwarfComments(dwarf.DwarfID);

            

            if (comments != null) 
            {
                foreach (Comment comment in comments)
                {
                    ShowCommentDTO commentDTO = new ShowCommentDTO();

                    commentDTO.ID = comment.CommentID;
                    commentDTO.Text = comment.Text;
                    commentDTO.AuthorID = comment.UserID;
                    commentDTO.AuthorName = comment.Author.Username;

                    dto.comments.CommentList.Add(commentDTO);
                }
                dto.comments.Count = comments.Count;
            }
            return dto;

        }


        public UpdateDwarfDTO GetDwarfToEdit(int id)
        {
            var dwarf = _dwarfRepository.GetDwarfByID(id);

            UpdateDwarfDTO dto = new UpdateDwarfDTO()
            {
                ID = id,
                Name= dwarf.Name,
                Description= dwarf.Description,
                ActivationCode = dwarf.ActivationCode
            };

            return dto;
        }

        public ListShowDwarvesDTO GetDwarves()
        {
            var dwarves = _dwarfRepository.GetDwarves();

            ListShowDwarvesDTO dwarvesDTO = new ListShowDwarvesDTO();

            foreach (var dwarf in dwarves) 
            {
                ShowDwarfDTO dwarfDTO = new ShowDwarfDTO();
                dwarfDTO.DwarfID = dwarf.DwarfID;
                dwarfDTO.Name = dwarf.Name;

                dwarvesDTO.DwarfList.Add(dwarfDTO);
            }

            dwarvesDTO.Count = dwarvesDTO.DwarfList.Count;

            return dwarvesDTO;
        }

        public ListShowDwarvesDTO GetVisitedDwarves(int userID)
        {
            var dwarves = _dwarfRepository.GetUserDwarves(userID);

            ListShowDwarvesDTO dwarvesList = new ListShowDwarvesDTO();


            foreach (var dwarf in dwarves)
            {

                ShowDwarfDTO dto = new ShowDwarfDTO()
                {
                    DwarfID = dwarf.DwarfID,
                    Name = dwarf.Name
                };

                dwarvesList.DwarfList.Add(dto);

            }
            dwarvesList.Count = dwarvesList.DwarfList.Count;

            return dwarvesList;
        }

        public void UpdateDwarf(UpdateDwarfDTO dto)
        {
            var dwarf = _dwarfRepository.GetDwarfByID(dto.ID);
            if (dwarf == null)
            {
                return;
            }

            dwarf.Name = dto.Name;
            dwarf.Description = dto.Description;
            dwarf.ActivationCode = dto.ActivationCode;

            _dwarfRepository.UpdateDwarf(dwarf);
        }
    }
}
