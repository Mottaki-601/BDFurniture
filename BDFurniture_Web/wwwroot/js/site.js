// BD Furniture - Main JavaScript
'use strict';

// ===== NAVBAR SCROLL BEHAVIOR =====
const navbar = document.querySelector('.navbar-main');
if (navbar) {
    window.addEventListener('scroll', () => {
        navbar.classList.toggle('scrolled', window.scrollY > 60);
    }, { passive: true });
}

// ===== MOBILE MEGA MENU TOGGLE =====
document.querySelectorAll('.nav-mega-dropdown > .nav-link').forEach(link => {
    link.addEventListener('click', function(e) {
        if (window.innerWidth < 992) {
            e.preventDefault();
            const parent = this.closest('.nav-mega-dropdown');
            parent.classList.toggle('open');
        }
    });
});

// ===== SCROLL TO TOP =====
const scrollTopBtn = document.querySelector('.scroll-top-btn');
if (scrollTopBtn) {
    window.addEventListener('scroll', () => {
        scrollTopBtn.classList.toggle('visible', window.scrollY > 300);
    }, { passive: true });
    scrollTopBtn.addEventListener('click', () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });
}

// ===== ANIMATE ON SCROLL =====
const aosElements = document.querySelectorAll('.aos-fade-up');
if (aosElements.length) {
    const observer = new IntersectionObserver(
        entries => entries.forEach(e => { if (e.isIntersecting) e.target.classList.add('visible'); }),
        { threshold: 0.1, rootMargin: '0px 0px -40px 0px' }
    );
    aosElements.forEach(el => observer.observe(el));
}

// ===== VIDEO SCROLL CAROUSEL (Drag) =====
function initDragScroll(trackSelector) {
    const track = document.querySelector(trackSelector);
    if (!track) return;
    let isDown = false, startX, scrollLeft;
    track.addEventListener('mousedown', e => {
        isDown = true;
        track.classList.add('dragging');
        startX = e.pageX - track.offsetLeft;
        scrollLeft = track.scrollLeft;
    });
    document.addEventListener('mouseup', () => { isDown = false; track.classList.remove('dragging'); });
    track.addEventListener('mouseleave', () => { isDown = false; track.classList.remove('dragging'); });
    track.addEventListener('mousemove', e => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - track.offsetLeft;
        track.scrollLeft = scrollLeft - (x - startX) * 1.5;
    });
}
initDragScroll('.video-scroll-track');

// Scroll buttons
document.querySelectorAll('[data-scroll-target]').forEach(btn => {
    btn.addEventListener('click', function() {
        const target = document.querySelector(this.dataset.scrollTarget);
        const dir = this.dataset.scrollDir === 'left' ? -1 : 1;
        if (target) target.scrollBy({ left: dir * 320, behavior: 'smooth' });
    });
});

// ===== YOUTUBE VIDEO MODAL =====
function openYouTubeModal(videoId) {
    const modal = document.getElementById('ytModal');
    const iframe = document.getElementById('ytIframe');
    if (modal && iframe) {
        iframe.src = `https://www.youtube.com/embed/${videoId}?autoplay=1&rel=0`;
        const bsModal = new bootstrap.Modal(modal);
        bsModal.show();
        modal.addEventListener('hidden.bs.modal', () => { iframe.src = ''; }, { once: true });
    }
}
window.openYouTubeModal = openYouTubeModal;

// ===== PRODUCT IMAGE GALLERY =====
document.querySelectorAll('.product-thumb').forEach(thumb => {
    thumb.addEventListener('click', function() {
        const mainImg = document.querySelector('.product-detail-img-main img');
        if (mainImg) {
            mainImg.src = this.dataset.imgSrc;
            mainImg.style.opacity = 0;
            setTimeout(() => mainImg.style.opacity = 1, 50);
        }
        document.querySelectorAll('.product-thumb').forEach(t => t.classList.remove('active'));
        this.classList.add('active');
    });
});
document.querySelectorAll('.product-detail-img-main img').forEach(img => {
    img.style.transition = 'opacity 0.3s ease';
});

// ===== STAR RATING (Interactive) =====
const ratingInputs = document.querySelectorAll('.rating-stars-interactive input[type=radio]');
ratingInputs.forEach(input => {
    input.addEventListener('change', function() {
        const hiddenInput = document.getElementById('ratingValue');
        if (hiddenInput) hiddenInput.value = this.value;
    });
});

// ===== TOAST NOTIFICATION =====
function showToast(message, type = 'success') {
    let toast = document.querySelector('.toast-bd');
    if (!toast) {
        toast = document.createElement('div');
        toast.className = 'toast-bd';
        document.body.appendChild(toast);
    }
    toast.className = `toast-bd ${type}`;
    toast.textContent = message;
    toast.classList.add('show');
    setTimeout(() => toast.classList.remove('show'), 3500);
}
window.showToast = showToast;

// ===== AJAX CONTACT FORM =====
const contactForm = document.getElementById('contactForm');
if (contactForm) {
    contactForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        const btn = this.querySelector('[type=submit]');
        const original = btn.innerHTML;
        btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Sending...';
        btn.disabled = true;
        try {
            const res = await fetch('/Home/SendContact', {
                method: 'POST',
                body: new FormData(this),
                headers: { 'X-Requested-With': 'XMLHttpRequest' }
            });
            const data = await res.json();
            if (data.success) {
                showToast('Message sent successfully! We will contact you soon.', 'success');
                this.reset();
            } else {
                showToast(data.message || 'Something went wrong!', 'error');
            }
        } catch {
            showToast('Network error. Please try again.', 'error');
        } finally {
            btn.innerHTML = original;
            btn.disabled = false;
        }
    });
}

// ===== NEWSLETTER FORM =====
const newsletterForm = document.getElementById('newsletterForm');
if (newsletterForm) {
    newsletterForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        const btn = this.querySelector('[type=submit]');
        btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i>';
        btn.disabled = true;
        try {
            const res = await fetch('/Home/Subscribe', {
                method: 'POST',
                body: new FormData(this),
                headers: { 'X-Requested-With': 'XMLHttpRequest' }
            });
            const data = await res.json();
            showToast(data.message || 'Subscribed successfully!', data.success ? 'success' : 'error');
            if (data.success) this.reset();
        } catch {
            showToast('Network error.', 'error');
        } finally {
            btn.innerHTML = 'Subscribe';
            btn.disabled = false;
        }
    });
}

// ===== PRODUCT INQUIRY FORM =====
const inquiryForm = document.getElementById('inquiryForm');
if (inquiryForm) {
    inquiryForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        const btn = this.querySelector('[type=submit]');
        const original = btn.innerHTML;
        btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Sending...';
        btn.disabled = true;
        try {
            const res = await fetch('/Product/SendInquiry', {
                method: 'POST',
                body: new FormData(this),
                headers: { 'X-Requested-With': 'XMLHttpRequest' }
            });
            const data = await res.json();
            if (data.success) {
                showToast('Inquiry sent! We will reach out soon.', 'success');
                const modal = bootstrap.Modal.getInstance(document.getElementById('inquiryModal'));
                if (modal) modal.hide();
                this.reset();
            } else {
                showToast(data.message || 'Error sending inquiry.', 'error');
            }
        } catch {
            showToast('Network error.', 'error');
        } finally {
            btn.innerHTML = original;
            btn.disabled = false;
        }
    });
}

// ===== LAZY LOAD IMAGES =====
if ('IntersectionObserver' in window) {
    const imgObserver = new IntersectionObserver(
        entries => entries.forEach(e => {
            if (e.isIntersecting) {
                const img = e.target;
                if (img.dataset.src) {
                    img.src = img.dataset.src;
                    img.removeAttribute('data-src');
                    imgObserver.unobserve(img);
                }
            }
        }),
        { rootMargin: '200px' }
    );
    document.querySelectorAll('img[data-src]').forEach(img => imgObserver.observe(img));
}

// ===== HERO VIDEO FALLBACK =====
const heroVideo = document.querySelector('.hero-video-bg');
if (heroVideo) {
    heroVideo.addEventListener('error', () => {
        heroVideo.style.display = 'none';
    });
}

