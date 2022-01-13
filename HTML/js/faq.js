const btn_customer = document.querySelector('.btn-customer');
const btn_service_provider = document.querySelector('.btn-service-provider');
const customer_faq = document.querySelector('.customer-faq');;
const service_provider_faq = document.querySelector('.service-provider-faq');

btn_customer.addEventListener('click',(e)=>{
      customer_faq.style.display = 'block';
      service_provider_faq.style.display = 'none';
      btn_customer.classList.add('active');
      btn_service_provider.classList.remove('active');
})

btn_service_provider.addEventListener('click',(e)=>{
      customer_faq.style.display = 'none';
      service_provider_faq.style.display = 'block';
      btn_customer.classList.remove('active');
      btn_service_provider.classList.add('active');
})