﻿using AutoMapper;
using CustomerOnboardingService.DTOs;
using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOnboardingService.Controllers
{


	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IOtp _otp;
		private readonly ICustomer _customer;
		private readonly IMapper _mapper;


		public CustomerController(IOtp otp, ICustomer customer, IMapper mapper)
		{
			_otp = otp;
			_customer = customer;
			_mapper = mapper;
		}

		[HttpPost]
		[Route("OnbordingCustomerData")]
		public async Task<IActionResult> OnbordingCustomerData([FromBody]CustomerDTO model) {

			 var resp=await _customer.OnBoardCustomer(model);
			return Ok(resp);
		}

		[HttpGet]
		[Route("GetAllOnboarded")]
		public async Task<IActionResult> GetAllOnboarded()
		{
			List<CustomerDTO> response =await _customer.GetOnBoardedCustomer();

			return Ok(response);
		}

		[HttpPost]
		[Route("GetNewOtp")]
		public async Task<IActionResult> GetNewOtp([FromBody]GetOtpDTO model)
		{
			var response = await _otp.GenerateNewOtp(model);

			return Ok(response);
		}

		[HttpPost]
		[Route("VerifyCustomer")]
		public async Task<IActionResult> VerifyCustomer([FromBody] VerifyCustomerDto model)
		{
			var response = await _customer.VerifyCustomer(model);
			return Ok(response);
		}
	}
}
