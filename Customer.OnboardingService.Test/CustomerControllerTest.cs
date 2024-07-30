using AutoMapper;
using CustomerOnboardingService.Controllers;
using CustomerOnboardingService.Data;
using CustomerOnboardingService.DTOs;
using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using CustomerOnboardingService.InterfaceRepositories.Repositories;
using CustomerOnboardingService.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerOnboardingService.Test
{
	public class CustomerControllerTest
	{
		private readonly CustomerController _customerController;
		private readonly ILogger<CustomerRepository> _logger;
		private readonly IOtp _otpService;
		private readonly IMapper _mapper;
		private readonly ICustomer _customer;
		public CustomerControllerTest(CustomerController customerController = null)
		{
			_customer = A.Fake<ICustomer>();
			_logger = A.Fake<ILogger<CustomerRepository>>();
			_otpService = A.Fake<IOtp>();
			_mapper = A.Fake<IMapper>();
			_customerController =new  CustomerController(_otpService, _customer, _mapper);
		}

		[Fact]
		public  void CustomerControllerTest_should_ReturnSuccess()
		{
			//arrange


			var customerDto = A.Fake<CustomerDTO>();


			A.CallTo(() =>  _customer.OnBoardCustomer(customerDto)).Returns(customerDto);

			//act

			var result =
				_customerController.OnbordingCustomerData(customerDto);


			//assert

			result.Should().BeOfType<Task<IActionResult>>();
	




		}

		[Fact]

		public void CustomerController_GetAllOnboarded_should_returnAllOnboarded()
		
		{

			//arrangement
			List<CustomerDTO>? customers =
				A.Fake<List<CustomerDTO>>();
			   A.CallTo(() => _customer.GetOnBoardedCustomer()).Returns(customers);

			//act

			var result = _customerController.GetAllOnboarded();

			//assertion

			result.Should().BeOfType<Task<IActionResult>>();
		}
	}
}