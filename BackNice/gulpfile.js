var gulp = require('gulp'),
    sass = require('gulp-sass'),
    uglify = require('gulp-uglify'),
    plumber = require('gulp-plumber'),
    autoprefixer = require('gulp-autoprefixer'),
    cleanCSS = require('gulp-clean-css'),
    rename = require('gulp-rename'),
    rimraf = require('gulp-rimraf');
var autoprefixerList = [
    'Chrome >= 45',
    'Firefox ESR',
    'Edge >= 12',
    'Explorer >= 10',
    'iOS >= 9',
    'Safari >= 9',
    'Android >= 4.4',
    'Opera >= 30'
];
var config = {
    server: {
        baseDir: './wwwwroot/'
    },
    notify: false
};
var Paths = {
    DIST: './wwwroot/',
    CSS: './wwwroot/css/',
    CSS_TOOLKIT_SOURCES: './node_modules/material-kit/assets/css/*.css',
    build: {
        js: './wwwroot/js/',
        css: './wwwroot/css/',
        img: './wwwroot/img/',
        fonts: './wwwroot/fonts/'
    },
    src: {
        js: '/assets/js/*.js',
        sass: '/assets/sass/*.sass',
        img: '/assets/img/*.*',
        fonts: '/assets/fonts/*.*'
    },
    clean: './wwwroot/css/*'
};
gulp.task('css:compileBootstrap', function () {
    return gulp.src(Paths.CSS_TOOLKIT_SOURCES)
        .pipe(gulp.dest(Paths.DIST +"material-kit/lib/css/"));
});
gulp.task('js:compileMaterialKit', function () {
    return gulp.src('node_modules/material-kit/assets/js/*.js')
        .pipe(gulp.dest('wwwroot/lib/material-kit/js'));
});
gulp.task('js:compileCore', function () {
    return gulp.src('node_modules/material-kit/assets/js/core/*.js')
        .pipe(gulp.dest('wwwroot/lib/material-kit/js'));
});
gulp.task('js:compilePlugins', function () {
    return gulp.src('node_modules/material-kit/assets/js/plugins/*.js')
        .pipe(gulp.dest('wwwroot/lib/material-kit/js'));
});
gulp.task('compile-sass', function () {
    return gulp.src(Paths.src.sass)
        .pipe(plumber())
        .pipe(sass())
        .pipe(autoprefixer({
            browsers: autoprefixerList
        }))
        .pipe(gulp.dest(Paths.build.css))
        .pipe(rename({ suffix: '.min' }))
        .pipe(cleanCSS())
        .pipe(gulp.dest(Paths.build.css))
        .pipe(webserver.reload({ stream: true }));;
});

gulp.task('watch', function () {
    gulp.watch(Paths.src.sass, gulp.series('compile-sass'));
});