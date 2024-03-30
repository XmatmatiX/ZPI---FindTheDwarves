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

        public int ClaimDwarf(ClaimDwarfDTO dto, int userID)
        {

            var dwarf = _dwarfRepository.GetDwarfByCode(dto.ActivationCode);

            if (dwarf == null)
            {
                return -1;
            }

            var userDwarf = new UserDwarf()
            {
                UserID = userID,
                DwarfID = dwarf.DwarfID
            };

            if (_dwarfRepository.CheckClaim(userDwarf) == true)
            {
                return -2;
            }

            _dwarfRepository.ClaimDwarf(userDwarf);

            return dwarf.DwarfID;
        }

        public DwarfDetailsDTO GetDwarfByName(string name)
        {
            var dwarf = _dwarfRepository.GetDwarfByName(name);

            if (dwarf == null) 
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

                    dto.comments.CommentList.Add(commentDTO);
                }
                dto.comments.Count = comments.Count;
            }
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
    }
}
