using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Controllers;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Namespace
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : BaseController
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            this._memberService = memberService;
        }

        [HttpGet]
        public ActionResult<List<MemberDto>> Get()
        {
            return Ok(_memberService.GetMembers());
        }

        [HttpGet("{username}")]
        public ActionResult<MemberDto> Get(string username)
        {
            var member = _memberService.GetMemberByUsername(username);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
    }
}