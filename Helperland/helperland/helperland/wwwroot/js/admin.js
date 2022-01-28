const btn_options = document.querySelectorAll('.btn-option');
const action_options = document.querySelectorAll('.action-options');

const submenus = document.querySelectorAll('.submenu');
const submenu_list = document.querySelectorAll('.submenu-list');
const submenu_img = document.querySelectorAll('.submenu-img');

window.addEventListener('click',(e) => {
      action_options_hide();
});

const action_options_hide = () => {
      action_options.forEach((action_option)=>{
            action_option.style.display = 'none';
      });
}

const hideShowActionOptions = (e,index) => {
      action_options_hide();
      const style = window.getComputedStyle(action_options[index]);
      if(style.display === 'none'){
            action_options[index].style.display = 'block';
      }
      else{
            action_options[index].style.display = 'none';
      }
      e.stopPropagation();
}

const submenuShowHide = (index) => {
      const style = window.getComputedStyle(submenu_list[index]);
      if(style.display === 'none'){
            submenu_list[index].style.display = 'block';
            submenus[index].classList.add('submenu-hover');
      }
      else{
            submenu_list[index].style.display = 'none';
            submenus[index].classList.remove('submenu-hover');
      }
}

btn_options.forEach((btn_option,index)=>{
      btn_option.addEventListener('click',(e) => hideShowActionOptions(e,index));
});

submenus.forEach((submenu,index)=>{
      submenu.addEventListener('click',(e)=> submenuShowHide(index));
});