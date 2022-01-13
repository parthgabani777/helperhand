const profile_dropdown = document.querySelector('.profile-dropdown');
const admin_icon = document.querySelector('.admin-icon');

admin_icon.addEventListener('click',(e) => {
      const style = window.getComputedStyle(profile_dropdown);
      if(style.display === 'none'){
            profile_dropdown.style.display = 'block';
      }
      else{
            profile_dropdown.style.display = 'none';
      }
      e.stopPropagation();
})

window.addEventListener('click',()=>{
      profile_dropdown.style.display = 'none';
})