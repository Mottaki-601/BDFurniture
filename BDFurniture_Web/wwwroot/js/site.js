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
initDragScroll('#shortsTrack');

// Scroll buttons
document.querySelectorAll('[data-scroll-target]').forEach(btn => {
    btn.addEventListener('click', function() {
        const target = document.querySelector(this.dataset.scrollTarget);
        const dir = this.dataset.scrollDir === 'left' ? -1 : 1;
        if (target) {
            target.scrollBy({ left: dir * 320, behavior: 'smooth' });
            if (dir > 0 && target.id === 'shortsTrack' && typeof window.loadMoreShorts === 'function') {
                if (target.scrollLeft + target.clientWidth >= target.scrollWidth - 600) {
                    window.loadMoreShorts();
                }
            }
        }
    });
});

// ===== YOUTUBE VIDEO MODAL =====
function openYouTubeModal(videoId) {
    const modal = document.getElementById('ytModal');
    const iframe = document.getElementById('ytIframe');
    if (modal && iframe) {
        iframe.src = `https://www.youtube.com/embed/${videoId}?autoplay=1&rel=0`;
        const bsModal = bootstrap.Modal.getOrCreateInstance(modal);
        bsModal.show();
        modal.addEventListener('hidden.bs.modal', () => { iframe.src = ''; }, { once: true });
    }
}
window.openYouTubeModal = openYouTubeModal;

// ===== DEDICATED SHORTS REEL MODAL PLAYER =====
let currentShortIndex = 0;

function openShortsModal(videoId, title, index) {
    const modal = document.getElementById('shortsModal');
    const iframe = document.getElementById('shortsIframe');
    const titleEl = document.getElementById('shortsModalTitle');
    const ytLink = document.getElementById('shortsModalYtLink');
    if (!modal || !iframe) return;

    currentShortIndex = typeof index === 'number' ? index : 0;
    iframe.src = `https://www.youtube.com/embed/${videoId}?autoplay=1&rel=0&modestbranding=1&playsinline=1`;
    if (titleEl) titleEl.textContent = title || 'BD Furniture Short';
    if (ytLink) ytLink.href = `https://www.youtube.com/shorts/${videoId}`;

    const bsModal = bootstrap.Modal.getOrCreateInstance(modal);
    bsModal.show();

    modal.addEventListener('hidden.bs.modal', () => {
        iframe.src = '';
    }, { once: true });
}
window.openShortsModal = openShortsModal;

function navigateShort(direction) {
    if (!window.shortsData || !window.shortsData.length) return;

    if (direction > 0 && currentShortIndex >= window.shortsData.length - 2 && typeof window.loadMoreShorts === 'function') {
        window.loadMoreShorts();
    }

    currentShortIndex = (currentShortIndex + direction + window.shortsData.length) % window.shortsData.length;
    const nextItem = window.shortsData[currentShortIndex];
    if (nextItem) {
        openShortsModal(nextItem.videoId, nextItem.title, currentShortIndex);
    }
}
window.navigateShort = navigateShort;

// ===== PRODUCT IMAGE GALLERY (In-Box Slider & Deep Zoom Modal) =====
function initProductGallery() {
    const track = document.getElementById('galleryTrack');
    if (!track) return;
    if (track.dataset.galleryInit === 'true') return;
    track.dataset.galleryInit = 'true';

    const slides = track.querySelectorAll('.gallery-slide');
    const total = slides.length;
    if (total === 0) return;

    let currentIdx = 0;
    const prevBtn = document.getElementById('galleryPrevBtn');
    const nextBtn = document.getElementById('galleryNextBtn');
    const dots = document.querySelectorAll('.gallery-dot');
    const idxCounter = document.getElementById('currentImgIdx');

    function goToSlide(index) {
        currentIdx = (index + total) % total;
        const slideWidth = track.clientWidth;
        track.scrollTo({
            left: currentIdx * slideWidth,
            behavior: 'smooth'
        });
        updateUI(currentIdx);
    }

    function updateUI(index) {
        if (idxCounter) idxCounter.textContent = index + 1;
        dots.forEach((dot, i) => {
            dot.classList.toggle('active', i === index);
        });
    }

    // Only configure multi-slide controls when more than 1 image exists
    if (total > 1) {
        if (prevBtn) {
            prevBtn.addEventListener('click', (e) => {
                e.preventDefault();
                e.stopPropagation();
                goToSlide(currentIdx - 1);
            });
        }

        if (nextBtn) {
            nextBtn.addEventListener('click', (e) => {
                e.preventDefault();
                e.stopPropagation();
                goToSlide(currentIdx + 1);
            });
        }

        dots.forEach(dot => {
            dot.addEventListener('click', (e) => {
                e.preventDefault();
                e.stopPropagation();
                const targetIdx = parseInt(dot.dataset.index, 10);
                if (!isNaN(targetIdx)) goToSlide(targetIdx);
            });
        });

        // Mobile touch swipe & scroll detection (passive for 60fps performance)
        let scrollTimeout;
        track.addEventListener('scroll', () => {
            clearTimeout(scrollTimeout);
            scrollTimeout = setTimeout(() => {
                const slideWidth = track.clientWidth;
                if (slideWidth > 0) {
                    const detectedIdx = Math.round(track.scrollLeft / slideWidth);
                    const clampedIdx = Math.max(0, Math.min(total - 1, detectedIdx));
                    if (clampedIdx !== currentIdx) {
                        currentIdx = clampedIdx;
                        updateUI(currentIdx);
                    }
                }
            }, 50);
        }, { passive: true });

        // Keyboard arrow keys navigation when gallery box is focused
        const box = track.closest('.product-gallery-box');
        if (box) {
            box.addEventListener('keydown', (e) => {
                if (e.key === 'ArrowLeft') {
                    e.preventDefault();
                    goToSlide(currentIdx - 1);
                } else if (e.key === 'ArrowRight') {
                    e.preventDefault();
                    goToSlide(currentIdx + 1);
                }
            });
        }
    }

    // ===== FLOATING ZOOM & DEEP PAN (Desktop Scroll/Click & Mobile Pinch/Drag) =====
    const zoomModal = document.getElementById('galleryZoomModal');
    const zoomContent = document.getElementById('galleryZoomContent');
    const zoomImg = document.getElementById('galleryZoomImg');
    const zoomBackdrop = document.getElementById('galleryZoomBackdrop');
    const zoomClose = document.getElementById('galleryZoomClose');
    const zoomInBtn = document.getElementById('galleryZoomInBtn');
    const zoomOutBtn = document.getElementById('galleryZoomOutBtn');
    const zoomResetBtn = document.getElementById('galleryZoomResetBtn');

    // Zoom & Pan state
    let scale = 1;
    let panX = 0;
    let panY = 0;
    const MIN_SCALE = 1;
    const MAX_SCALE = 4;
    let isDragging = false;
    let dragStartX = 0;
    let dragStartY = 0;
    let initialPanX = 0;
    let initialPanY = 0;
    let dragDistance = 0;

    // Mobile touch state
    let initialTouchDistance = 0;
    let initialTouchScale = 1;
    let isTouchPanning = false;
    let touchStartX = 0;
    let touchStartY = 0;
    let lastTapTime = 0;

    function applyTransform(withTransition = false) {
        if (!zoomImg) return;
        if (withTransition) {
            zoomImg.style.transition = 'transform 0.22s cubic-bezier(0.2, 0, 0.2, 1)';
        } else {
            zoomImg.style.transition = 'none';
        }
        zoomImg.style.transform = `translate3d(${panX}px, ${panY}px, 0) scale(${scale})`;

        if (zoomContent) {
            if (scale > 1) {
                zoomContent.style.cursor = isDragging || isTouchPanning ? 'grabbing' : 'grab';
            } else {
                zoomContent.style.cursor = 'zoom-in';
            }
        }
    }

    function clampPan() {
        if (!zoomContent || !zoomImg || scale <= 1) {
            panX = 0;
            panY = 0;
            return;
        }
        const contentRect = zoomContent.getBoundingClientRect();
        // Constrain pan within the designated frame area
        const maxPanX = Math.max(0, (contentRect.width * (scale - 1)) / 2 + 50);
        const maxPanY = Math.max(0, (contentRect.height * (scale - 1)) / 2 + 50);

        panX = Math.min(maxPanX, Math.max(-maxPanX, panX));
        panY = Math.min(maxPanY, Math.max(-maxPanY, panY));
    }

    function setZoom(newScale, focalX = null, focalY = null, withTransition = true) {
        const clampedScale = Math.min(MAX_SCALE, Math.max(MIN_SCALE, newScale));
        if (clampedScale <= 1) {
            scale = 1;
            panX = 0;
            panY = 0;
        } else {
            if (focalX !== null && focalY !== null && zoomContent) {
                const rect = zoomContent.getBoundingClientRect();
                const relX = focalX - (rect.left + rect.width / 2);
                const relY = focalY - (rect.top + rect.height / 2);
                const ratio = clampedScale / scale;
                panX = panX - relX * (ratio - 1);
                panY = panY - relY * (ratio - 1);
            }
            scale = clampedScale;
            clampPan();
        }
        applyTransform(withTransition);
    }

    function resetZoom(withTransition = true) {
        scale = 1;
        panX = 0;
        panY = 0;
        applyTransform(withTransition);
    }

    function openZoomModal() {
        if (!zoomModal || !zoomImg) return;
        const activeSlide = slides[currentIdx] || slides[0];
        const activeImg = activeSlide ? activeSlide.querySelector('img') : null;
        if (activeImg && activeImg.src) {
            zoomImg.src = activeImg.src;
            resetZoom(false);
            zoomModal.classList.add('show');
            zoomModal.setAttribute('aria-hidden', 'false');
            document.body.style.overflow = 'hidden';
        }
    }

    function closeZoomModal() {
        if (!zoomModal) return;
        zoomModal.classList.remove('show');
        zoomModal.setAttribute('aria-hidden', 'true');
        resetZoom(false);
        document.body.style.overflow = '';
    }

    // Modal close triggers
    if (zoomBackdrop) zoomBackdrop.addEventListener('click', closeZoomModal);
    if (zoomClose) zoomClose.addEventListener('click', (e) => {
        e.stopPropagation();
        closeZoomModal();
    });
    if (zoomModal) {
        zoomModal.addEventListener('click', function(e) {
            if (e.target === zoomModal || e.target === zoomBackdrop || e.target.classList.contains('gallery-zoom-container')) {
                closeZoomModal();
            }
        });
    }
    window.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') closeZoomModal();
    });

    // Toolbar controls
    if (zoomInBtn) {
        zoomInBtn.addEventListener('click', (e) => {
            e.stopPropagation();
            setZoom(scale + 0.5, null, null, true);
        });
    }
    if (zoomOutBtn) {
        zoomOutBtn.addEventListener('click', (e) => {
            e.stopPropagation();
            setZoom(scale - 0.5, null, null, true);
        });
    }
    if (zoomResetBtn) {
        zoomResetBtn.addEventListener('click', (e) => {
            e.stopPropagation();
            resetZoom(true);
        });
    }

    // Desktop: Mouse wheel & Mouse drag/pan & Click toggle
    if (zoomContent) {
        zoomContent.addEventListener('wheel', (e) => {
            e.preventDefault();
            const delta = e.deltaY < 0 ? 0.35 : -0.35;
            setZoom(scale + delta, e.clientX, e.clientY, false);
        }, { passive: false });

        zoomContent.addEventListener('mousedown', (e) => {
            if (e.button !== 0) return;
            e.preventDefault();
            isDragging = true;
            dragStartX = e.clientX;
            dragStartY = e.clientY;
            initialPanX = panX;
            initialPanY = panY;
            dragDistance = 0;
            applyTransform(false);
        });

        window.addEventListener('mousemove', (e) => {
            if (!isDragging) return;
            const dx = e.clientX - dragStartX;
            const dy = e.clientY - dragStartY;
            dragDistance = Math.hypot(dx, dy);

            if (scale > 1) {
                panX = initialPanX + dx;
                panY = initialPanY + dy;
                clampPan();
                applyTransform(false);
            }
        });

        window.addEventListener('mouseup', (e) => {
            if (!isDragging) return;
            isDragging = false;
            applyTransform(false);

            // Clean click without drag
            if (dragDistance < 6) {
                if (scale === 1) {
                    setZoom(2.2, e.clientX, e.clientY, true);
                } else {
                    resetZoom(true);
                }
            }
        });

        // Mobile: Pinch-to-zoom, single finger drag, double-tap toggle
        zoomContent.addEventListener('touchstart', (e) => {
            if (e.touches.length === 2) {
                e.preventDefault();
                isTouchPanning = false;
                initialTouchDistance = Math.hypot(
                    e.touches[0].clientX - e.touches[1].clientX,
                    e.touches[0].clientY - e.touches[1].clientY
                );
                initialTouchScale = scale;
            } else if (e.touches.length === 1) {
                const touch = e.touches[0];
                const now = Date.now();
                if (now - lastTapTime < 320) {
                    e.preventDefault();
                    if (scale > 1) {
                        resetZoom(true);
                    } else {
                        setZoom(2.2, touch.clientX, touch.clientY, true);
                    }
                    lastTapTime = 0;
                    return;
                }
                lastTapTime = now;

                if (scale > 1) {
                    isTouchPanning = true;
                    touchStartX = touch.clientX;
                    touchStartY = touch.clientY;
                    initialPanX = panX;
                    initialPanY = panY;
                }
            }
        }, { passive: false });

        zoomContent.addEventListener('touchmove', (e) => {
            if (e.touches.length === 2 && initialTouchDistance > 0) {
                e.preventDefault();
                const currentDistance = Math.hypot(
                    e.touches[0].clientX - e.touches[1].clientX,
                    e.touches[0].clientY - e.touches[1].clientY
                );
                const factor = currentDistance / initialTouchDistance;
                setZoom(initialTouchScale * factor, null, null, false);
            } else if (e.touches.length === 1 && isTouchPanning && scale > 1) {
                e.preventDefault();
                const touch = e.touches[0];
                const dx = touch.clientX - touchStartX;
                const dy = touch.clientY - touchStartY;
                panX = initialPanX + dx;
                panY = initialPanY + dy;
                clampPan();
                applyTransform(false);
            }
        }, { passive: false });

        zoomContent.addEventListener('touchend', (e) => {
            if (e.touches.length < 2) {
                initialTouchDistance = 0;
            }
            if (e.touches.length === 0) {
                isTouchPanning = false;
                if (scale <= 1) resetZoom(true);
            }
        });
    }

    // Main Gallery Box: Clicking anywhere opens the floating zoom modal
    let trackStartX = 0;
    let trackStartY = 0;
    let trackMoved = false;

    track.addEventListener('mousedown', (e) => {
        trackStartX = e.clientX;
        trackStartY = e.clientY;
        trackMoved = false;
    });

    track.addEventListener('mousemove', (e) => {
        if (Math.abs(e.clientX - trackStartX) > 8 || Math.abs(e.clientY - trackStartY) > 8) {
            trackMoved = true;
        }
    });

    track.addEventListener('touchstart', (e) => {
        if (e.touches && e.touches[0]) {
            trackStartX = e.touches[0].clientX;
            trackStartY = e.touches[0].clientY;
            trackMoved = false;
        }
    }, { passive: true });

    track.addEventListener('touchmove', (e) => {
        if (e.touches && e.touches[0]) {
            if (Math.abs(e.touches[0].clientX - trackStartX) > 10 || Math.abs(e.touches[0].clientY - trackStartY) > 10) {
                trackMoved = true;
            }
        }
    }, { passive: true });

    track.addEventListener('click', (e) => {
        if (trackMoved) {
            trackMoved = false;
            return;
        }
        if (!e.target.closest('.gallery-nav-btn') && !e.target.closest('.gallery-dots')) {
            openZoomModal();
        }
    });
}
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initProductGallery);
} else {
    initProductGallery();
}

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


//Menue hide 
document.addEventListener("DOMContentLoaded", function () {

    const toggler = document.querySelector(".navbar-toggler");
    const menu = document.querySelector(".navbar-collapse");

    if (!toggler || !menu) return;

    document.addEventListener("click", function (event) {

        // Menu বর্তমানে open কিনা
        if (!menu.classList.contains("show")) {
            return;
        }

        // Toggle button অথবা menu-এর ভিতরে click করলে কিছু করবো না
        if (
            toggler.contains(event.target) ||
            menu.contains(event.target)
        ) {
            return;
        }

        // বাইরে click করলে menu বন্ধ
        const collapse = bootstrap.Collapse.getInstance(menu);

        if (collapse) {
            collapse.hide();
        }

    });

});