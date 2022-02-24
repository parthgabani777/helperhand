const navTabs = document.querySelectorAll('.nav-tabs .nav-item');
const navLinks = document.querySelectorAll('.nav-item .nav-link');
const steps = document.querySelectorAll('.steps');

const imgItems = document.querySelectorAll('.img-item');

// For changing active image
const changeImg = (e) => {
      if(e.currentTarget.classList.contains('active')) return e.currentTarget.classList.remove('active');
      return e.currentTarget.classList.add('active');
};

imgItems.forEach((imgItem,index)=>{
      imgItem.addEventListener('click',(e) => changeImg(e));
});

// For changing tab content
const changeStep = (e,index) => {
      navLinks.forEach((navLink,navLinkIndex) => {
            if(navLinkIndex <= index) return navLink.classList.add('active');
            return navLink.classList.remove('active');
      });
      steps.forEach((step,i)=>{
            if(i==index) return step.style.display = 'block';
            step.style.display = 'none';
      });
};

navTabs.forEach((navTab,index) => {
      navTab.addEventListener('click',(e) => changeStep(e,index));
});